using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDead : State<BossController>
{
    public BossDead(BossController boss, PlayerController player) : base(boss, player)
    {
    }

    public override void OnEnter(BossController boss)
    {
        Dead(boss);
    }

    public override void OnExit(BossController boss)
    {

    }

    public override void OnFixedUpdate(BossController boss)
    {

    }

    public override void OnUpdate(BossController boss)
    {
    }

    private void Dead(BossController boss)
    {
        boss.b_anim.SetTrigger("Dead");
      
    }

}
