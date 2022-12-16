using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLandingPoint : BossSkyState
{

    public BossLandingPoint(BossController boss, PlayerController player) : base(boss, player)
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
        base.OnUpdate(boss);
    }
}
