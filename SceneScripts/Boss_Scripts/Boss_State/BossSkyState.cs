using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkyState : State<BossController>
{

    private float moveHeight = 10f;
    private float flyHeight = 40f;
    private float checkHeight = 2f;
    private float flySpeed = 30f;
    private float flyDownSpeed = 4f;
    private float landSpeed = 10f;
    private float flightAccuracy = 1f;
    private float findSpeed = 4f;


    public BossSkyState(BossController boss, PlayerController player) : base(boss, player)
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
        CheckStateSky(boss);

        if (boss.IsState(BossController.eState.LandingPoint))
            boss.CheckDistanceLand();
    }

    // 스카이 스테이트를 확인 이동 및 회전 값 다음 스테이트를 정해 준다.
    private void CheckStateSky(BossController boss)
    {

        if (boss.IsState(BossController.eState.FlyAround))
        {
            boss.bossCam.Priority = 11;
            boss.destination = player.transform.position + new Vector3(0f, flyHeight, 0f);
            boss.TurnTo(flightAccuracy, boss.destination);
            FlyMove(boss, flySpeed);
        }

        if (boss.IsState(BossController.eState.LandingPoint))
        {
            boss.destination = boss.landPos;
            boss.destination.y = boss.transform.position.y;
            boss.TurnTo(findSpeed, boss.destination);
            FlyMove(boss, landSpeed);
        }

        if (boss.IsState(BossController.eState.Landing))
        {
            boss.bossCam.Priority = 9;
            FlyDown(boss, flyDownSpeed);
        }

    }


    public void FlyMove(BossController boss, float _velocity)
    {
        if (boss.transform.position.y >= moveHeight)
        {
            boss.transform.position += boss.transform.forward * _velocity * Time.deltaTime;
        }
    }

    private void FlyDown(BossController boss, float _downspeed)
    {
        if (boss.transform.position.y > checkHeight)
        {
            boss.transform.position -= new Vector3(0f, _downspeed, 0f) * _downspeed * Time.deltaTime;
        }
        if (boss.transform.position.y <= checkHeight)
        {
            boss.actNext = true;
        }
    }


}
