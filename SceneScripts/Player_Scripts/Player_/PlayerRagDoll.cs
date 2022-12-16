using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRagDoll : MonoBehaviour
{

    public PlayerController player;
    public GameObject character;
    public GameObject playerObj;
    private Animator anim;
    public GameObject spine;
    WaitForSeconds wait = new WaitForSeconds(2f);

    private float AnimTime => anim.GetCurrentAnimatorStateInfo(0).normalizedTime;

    private void Start()
    {
    
        anim = GetComponent<Animator>();

    }
    private void OnEnable()
    {
        StartCoroutine(WakeUpCoroutine());
    }

    private void OnDisable()
    {
        anim.enabled = false;
    }


    // Character의 위치를 ragdoll의 스파인 위치에설정 Getup 애니메이션을 이용하여 일어난다
    IEnumerator WakeUpCoroutine()
    {
        yield return wait;
        character.transform.position = spine.transform.position;
        anim.enabled = true;
        RayCasting();
        yield return new WaitUntil(() => AnimTime > 0.9f);
        ManageRagdoll.GetInstance().ChangeRagdoll(this.gameObject, playerObj.gameObject);
        player.ChangeState(PlayerController.eState.Move);
    }

    // ragdoll의 Spine에서 RayCast를 통해 누워있는 방향을 판단 실행할 애니메이션을 결정
    private void RayCasting()
    {
        RaycastHit hit;
        int layerMaskFloor = 1 << LayerMask.NameToLayer("Floor");

        Physics.Raycast(spine.transform.position, spine.transform.forward, out hit, Mathf.Infinity, layerMaskFloor);

        if (hit.distance <= 1f)
            anim.SetTrigger("GetUp_F");
        else
            anim.SetTrigger("GetUp_B");
        

    }


}
