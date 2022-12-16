using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInViewCheck : State<BossController>
{
    
    public BossInViewCheck(BossController boss, PlayerController player) : base(boss, player)
    {
    }

    public override void OnEnter(BossController boss)
    {
        boss.b_rigid.isKinematic = true;
    }

    public override void OnExit(BossController boss)
    {
        boss.b_rigid.isKinematic = false;

    }

    public override void OnFixedUpdate(BossController boss)
    {

    }

    public override void OnUpdate(BossController boss)
    {
        CheckViewAngle(boss);
    }

    // �þ߰� üũ
    private void CheckViewAngle(BossController boss)
    {
        float viewAngle = 30f;
        float rotSpeed = 3f;

        // ������ �̿� ������ transform.forward�� �������� ĳ������ ��ġ �ľ�
        Vector3 distanceToPlayer = (player.transform.position - boss.transform.position).normalized;
        float dot = Vector3.Dot(distanceToPlayer, boss.transform.forward);
        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

        // �þ߰� �ۿ� ���� �� 
        if (angle > viewAngle)
        {
            Quaternion rot = Quaternion.LookRotation(player.transform.position - boss.transform.position);
            boss.transform.rotation = Quaternion.Slerp(boss.transform.rotation, rot, rotSpeed * Time.deltaTime);
        }
        // �þ߰� �ȿ� ������ ��
        else
        {
            boss.ChangeState(BossController.eState.AttackState);
        }

    }

}
