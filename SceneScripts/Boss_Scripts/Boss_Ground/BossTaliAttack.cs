using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTaliAttack : BossAttackState
{
    public BossTaliAttack(BossController boss, PlayerController player) : base(boss, player)
    {

    }

    public override void OnEnter(BossController boss)
    {
        TailAtk(boss);
    }

    public override void OnExit(BossController boss)
    {
        
    }

    public override void OnFixedUpdate(BossController boss)
    {

    }

    public override void OnUpdate(BossController boss)
    {
        boss.CheckAnimation("Tail");
        base.OnUpdate(boss);
   
    }
    private void TailAtk(BossController boss)
    {
            boss.b_anim.SetTrigger("Tail");
    }


}