using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGroundState : State<BossController>
{
    private float findSpeed = 5f;

    public BossGroundState(BossController boss, PlayerController player) : base(boss, player)
    {
    }

    public override void OnEnter(BossController boss)
    {
    }

    public override void OnExit(BossController boss)
    {

    }

    public override void OnFixedUpdate(BossController boss)
    {
    }

    public override void OnUpdate(BossController boss)
    {
        boss.CheckDistanceGround();

        CheckStateGround(boss);

    }

    // �׶��� ������Ʈ�� Ȯ���ؼ� ȸ�� ���� �������ش�.
    private void CheckStateGround(BossController boss)
    {

        if (boss.IsState(BossController.eState.Wander))
        {
            boss.b_anim.applyRootMotion = false;
        }

        else if (boss.IsState(BossController.eState.Chase))
        {
            boss.destination = player.transform.position;
            boss.TurnTo(findSpeed, boss.destination);
        }

    }

}
