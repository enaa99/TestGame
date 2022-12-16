using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBite : BossAttackState
{
    public BossBite(BossController boss, PlayerController player) : base(boss, player)
    {

    }

    public override void OnEnter(BossController boss)
    {
        BiteTo(boss);
    }

    public override void OnExit(BossController boss)
    {
        boss.headCollider.enabled = false;
    }

    public override void OnFixedUpdate(BossController boss)
    {

    }

    public override void OnUpdate(BossController boss)
    {
        boss.CheckAnimation("Bite");
        base.OnUpdate(boss);
    }
    
    //애니메이션 실행 및 콜라이더 enable
    public void BiteTo(BossController boss)
    {
        boss.b_anim.SetTrigger("Bite");
        boss.headCollider.enabled = true;

    }


}