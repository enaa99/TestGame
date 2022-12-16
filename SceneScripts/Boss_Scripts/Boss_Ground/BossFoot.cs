using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFoot : BossAttackState
{
    public BossFoot(BossController boss, PlayerController player) : base(boss, player)
    {
    }

    public override void OnEnter(BossController boss)
    {
        Foot(boss);   
    }

    public override void OnExit(BossController boss)
    {
        boss.legCollider.enabled = false;
    }

    public override void OnFixedUpdate(BossController boss)
    {

    }

    public override void OnUpdate(BossController boss)
    {
        boss.CheckAnimation("Foot");
        base.OnUpdate(boss);
    }

    private void Foot(BossController boss)
    {
        boss.b_anim.SetTrigger("Foot");
        boss.legCollider.enabled = true;
    }


}