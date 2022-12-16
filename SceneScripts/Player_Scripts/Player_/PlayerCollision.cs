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
        // ragdoll�� �ƴ� Player�� �ൿ�� ��츸 ����
        if (!player.PlayerAnimation.activeSelf)  return;

        // SKill ��ƼŬ�� �浹�� ���
        if (other.CompareTag("Skill"))
        {
            player.ChangeState(PlayerController.eState.GetHit);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ragdoll�� �ƴ� Player�� �ൿ�� ��츸 ����
        if (!player.PlayerAnimation.activeSelf)  return;


        //������ Collider�� CompareTag�� ���� GC���� Cpu���� ���� �� ragdoll ����

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
