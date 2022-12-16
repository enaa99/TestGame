using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStart : State<BossController>
{
    public BossStart(BossController boss, PlayerController player) : base(boss, player)
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
        //애니메이션 진행 정도에 따라 VirtualCamera조정
        if(boss.AnimTime > 0.6f)
        {
            boss.startCam.Priority = 1;
        }

        // 애니메이션 진행 정도에 따라 스테이트 전환
        if(boss.AnimTime > 0.9f)
        {
            boss.ChangeState(BossController.eState.Wander);
        }

    }

}
