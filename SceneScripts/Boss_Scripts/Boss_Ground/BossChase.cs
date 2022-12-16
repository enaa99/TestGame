using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChase : BossGroundState
{

    public BossChase(BossController boss, PlayerController player) : base(boss, player)
    {
    }



    public override void OnEnter(BossController boss)
    {

        DoChase(boss);
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

    private void DoChase(BossController boss)
    {
        boss.b_anim.SetTrigger("Run");

    }

}
