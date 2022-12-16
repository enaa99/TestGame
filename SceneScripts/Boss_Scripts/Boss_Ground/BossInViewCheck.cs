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

    // 시야각 체크
    private void CheckViewAngle(BossController boss)
    {
        float viewAngle = 30f;
        float rotSpeed = 3f;

        // 내적을 이용 보스의 transform.forward를 기준으로 캐릭터의 위치 파악
        Vector3 distanceToPlayer = (player.transform.position - boss.transform.position).normalized;
        float dot = Vector3.Dot(distanceToPlayer, boss.transform.forward);
        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

        // 시야각 밖에 있을 때 
        if (angle > viewAngle)
        {
            Quaternion rot = Quaternion.LookRotation(player.transform.position - boss.transform.position);
            boss.transform.rotation = Quaternion.Slerp(boss.transform.rotation, rot, rotSpeed * Time.deltaTime);
        }
        // 시야각 안에 존재할 때
        else
        {
            boss.ChangeState(BossController.eState.AttackState);
        }

    }

}
