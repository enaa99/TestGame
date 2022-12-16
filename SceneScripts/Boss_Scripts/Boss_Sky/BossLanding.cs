using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLanding : BossSkyState
{
    public BossLanding(BossController boss, PlayerController player) : base(boss, player)
    {
    }

    public override void OnEnter(BossController boss)
    {
        Landing(boss);
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

    private void Landing(BossController boss)
    {
        boss.b_anim.SetTrigger("Land");
        boss.b_rigid.useGravity = true;

    }

}
