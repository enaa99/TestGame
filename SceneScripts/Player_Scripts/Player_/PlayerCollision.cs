using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerController player;
    public GameObject ragDoll;

    public GameObject playerObj;


    private void OnParticleCollision(GameObject other)
    {
        // ragdoll이 아닌 Player가 행동할 경우만 적용
        if (!player.PlayerAnimation.activeSelf)  return;

        // SKill 파티클에 충돌할 경우
        if (other.CompareTag("Skill"))
        {
            player.ChangeState(PlayerController.eState.GetHit);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ragdoll이 아닌 Player가 행동할 경우만 적용
        if (!player.PlayerAnimation.activeSelf)  return;


        //지정한 Collider를 CompareTag를 통해 GC관리 Cpu성능 제고 및 ragdoll 연출

        if (other.CompareTag("Head"))
        {
            player.boss.headCollider.enabled = false;
            ManageRagdoll.GetInstance().ChangeRagdoll(playerObj, ragDoll);
            ShakeVirtualCamera.GetInstance().OnShake(3f, 3f, 0.5f);
            
        }

        if (other.CompareTag("Leg"))
        {
            player.boss.legCollider.enabled = false;
            ManageRagdoll.GetInstance().ChangeRagdoll(playerObj, ragDoll);
            ShakeVirtualCamera.GetInstance().OnShake(3f, 3f, 0.5f);
        }

        if (other.CompareTag("Breath"))
        {
            player.boss.headCollider.enabled = false;
            player.ChangeState(PlayerController.eState.OnFire);
            ManageRagdoll.GetInstance().ChangeRagdoll(playerObj, ragDoll);
            ShakeVirtualCamera.GetInstance().OnShake(5f, 5f, 2f);

        }
    }
}
