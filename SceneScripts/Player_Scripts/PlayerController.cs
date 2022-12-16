using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public BossController boss;

    public enum eState
    {
        OnFire,             // �극�� �ǰ�
        PlayerManageMent,   // �Ŵ��� ������Ʈ
        GetHit,             // �ǰ�
        Move,               // �̵�
        Skill,              // ��ų ���
        ComboAttack_1,      // �޺�����_1
        ComboAttack_2,      // �޺�����_2
        ComboAttack_3       // �޺�����_3

    }

    [HideInInspector]
    public CinemachineComposer virtualComposer;
    [HideInInspector]
    public CharacterController characterController;
    [HideInInspector]
    public Rigidbody m_rigid;
    
    public CinemachineVirtualCamera virtualCamera;
    public Collider BladeCollider;
    public GameObject PlayerAnimation;

    [HideInInspector]
    public string curAnim;
    [HideInInspector]
    public float AnimTime => p_anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    [HideInInspector]
    public bool AnimName => p_anim.GetCurrentAnimatorStateInfo(0).IsName(curAnim);


    [HideInInspector]
    public Animator p_anim;

   [HideInInspector]
    public StateMachine<PlayerController> m_sm;
    
    private Dictionary<eState, State<PlayerController>> 
        m_states = new Dictionary<eState, State<PlayerController>>();


    private void Start()
    {
        virtualComposer = virtualCamera.GetCinemachineComponent< Cinemachine.CinemachineComposer >();
        p_anim = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();
        m_rigid = GetComponent<Rigidbody>();

        m_states.Add(eState.Move,               new PlayerMove(boss,this));
        m_states.Add(eState.ComboAttack_1,      new PlayerComboAttack_1(boss, this));
        m_states.Add(eState.ComboAttack_2,      new PlayerComboAttack_2(boss, this));
        m_states.Add(eState.ComboAttack_3,      new PlayerComboAttack_3(boss, this));
        m_states.Add(eState.PlayerManageMent,   new PlayerManagement(boss, this));
        m_states.Add(eState.Skill,              new PlayerSkill(boss, this));
        m_states.Add(eState.GetHit,             new PlayerGetHit(boss, this));
        m_states.Add(eState.OnFire, new PlayerOnFire(boss, this));

        m_sm = new StateMachine<PlayerController>(this, null);

        ChangeState(eState.Move);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    
    }

    private void Update()
    {
        m_sm.OnUpdate();

    }

    private void FixedUpdate()
    {
        m_sm.OnFixedUpdate();
    }

    //StateMachine�� �̿� ������Ʈ ��ȯ
    public void ChangeState(eState state)
    {
        Debug.LogWarning(state);
        m_sm.SetState(m_states[state]);
    }

    // ���� ������Ʈ�� üũ�� �� �ִ� �Լ�
    public bool IsState(eState state)
    {
        return m_sm.CurState == m_states[state];
    }
  


}
