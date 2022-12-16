using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetHit : PlayerManagement
{
    public PlayerGetHit(BossController boss, PlayerController player) : base(boss, player)
    {

    }

    public override void OnEnter(PlayerController player)
    {
        OnHit(player);
    }

    public override void OnExit(PlayerController player)
    {

    }

    public override void OnFixedUpdate(PlayerController player)
    {

    }

    public override void OnUpdate(PlayerController player)
    {
        base.OnUpdate(player);
    }
    
    private void OnHit(PlayerController player)
    {
        player.p_anim.SetTrigger("GetHit");
    }

}
