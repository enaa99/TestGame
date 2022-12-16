using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWander : BossGroundState
{
    public BossWander(BossController boss, PlayerController player) : base(boss, player)
    {
    }

 

    public override void OnEnter(BossController boss)
    {
        DoWander(boss);
    }

    public override void OnExit(BossController boss)
    {
        boss.b_anim.applyRootMotion = true;
        boss.transform.SetParent(null);
        boss.track.SetActive(false);
    }

    public override void OnFixedUpdate(BossController boss)
    {

    }

    public override void OnUpdate(BossController boss)
    {
        base.OnUpdate(boss);
    }

    private void DoWander(BossController boss)
    {
        boss.b_anim.SetTrigger("Walk");
        boss.dollyCart.m_Speed = 3;
    }

}
