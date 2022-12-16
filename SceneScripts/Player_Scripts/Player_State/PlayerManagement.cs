using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagement : State<PlayerController>
{

    public float h, v;
    public float m_h, m_v;
    public float gravity = -9.81f; 
    public Vector3 moveVec = Vector3.zero;
    public Vector3 wantVec;
    public Vector3 dodgeVec;
    public float moveSpeed;
    public Quaternion Cam => player.virtualComposer.FollowTargetRotation;
    public float mouseXSpeed = 150f;
    public float dodgeSpeed = 8f;

    public PlayerManagement(BossController boss, PlayerController player) : base(boss, player)
    {
    }

    public override void OnEnter(PlayerController player)
    {

    }

    public override void OnExit(PlayerController player)
    {
        player.BladeCollider.enabled = false;
    }

    public override void OnFixedUpdate(PlayerController player)
    {

    }

    public override void OnUpdate(PlayerController player)
    {
        //레그돌 상태일 때 움직임 제한
        if (!player.PlayerAnimation.activeSelf)
            return;


        GroundCheck(player);
        DoMoveController(player);


        if (!player.IsState(PlayerController.eState.Move))
        {
            player.ChangeState(PlayerController.eState.Move);
        }

        if (Input.GetMouseButtonDown(0) && !player.IsState(PlayerController.eState.ComboAttack_1))
        {
            player.ChangeState(PlayerController.eState.ComboAttack_1);
        }

        if (Input.GetMouseButtonDown(1))
        {
            player.ChangeState(PlayerController.eState.Skill);
        }
       
    }

    #region characterController
    public void DoMoveController(PlayerController player)
    {

        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        moveVec = new Vector3(h, moveVec.y, v);

        // 카메라가 보고있는 방향을 기준을 방향키로 따라 이동
        wantVec = Cam * moveVec;

        // 이동 속도 설정 (앞으로 이동할때만 10, 나머지 2)
        moveSpeed = (v > 0 && h == 0) ? 8.0f : 2.0f;

        player.characterController.Move(wantVec * moveSpeed * Time.deltaTime);

       
        // 마우스 방향에 따라 카메라 회전
        m_v += Input.GetAxis("Mouse X") * mouseXSpeed * Time.deltaTime;
        m_h += Input.GetAxis("Mouse Y") * 1f * Time.deltaTime;

        m_h = ClampAngle(m_h, 0.5f, 2f);
        player.virtualComposer.m_ScreenY = m_h;
        player.transform.rotation = Quaternion.Euler(0, m_v, 0);


    }
    // 플레이어가 지상에 붙어 있는지 여부 체크
    private void GroundCheck(PlayerController playerController)
    {
        if (player.characterController.isGrounded == false)
        {
            moveVec.y += gravity + Time.deltaTime;
        }

    }
    // 마우스의 회전값 제한
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }
    #endregion
    
    #region Attack
    // 콤보 어택 
    public void ComboAtkCheck(PlayerController player, PlayerController.eState state, string _animName)
    {
        player.curAnim = _animName;
        // 애니메이션 진행시간이 60%이상이고 공격버튼을 눌렀을 때 상태 전환
        if(player.AnimName && player.AnimTime > 0.6f && Input.GetMouseButtonDown(0))
        {
            player.ChangeState(state);
        }
        // 애니메이션 시간이 끝나면 무브스테이트로 복귀
        else if(player.AnimName && player.AnimTime > 0.9f)
        {
            player.ChangeState(PlayerController.eState.Move);
            player.p_anim.SetTrigger("Move");
        }

    }
    // 애니메이션의 진행 여부 체크
    public void CheckAnim(PlayerController player, string _animName)
    {
        player.curAnim = _animName;

        if(player.AnimName && player.AnimTime >0.9f)
        {
            player.ChangeState(PlayerController.eState.Move);
            player.p_anim.SetTrigger("Move");
        }
    }

    // 검의 collider를 애니메이션의 진행 정도에 따라 끄고 켜주는 역할을 수행
    public void BladeCollision(PlayerController player, string _animName)
    {
        player.curAnim = _animName;

        if((player.AnimName && player.AnimTime >=0.5f) && (player.AnimName && player.AnimTime <= 0.8f))
        {
            player.BladeCollider.enabled = true;
        }

        else
        {
            player.BladeCollider.enabled = false;
        }
    }
    #endregion
 
}
