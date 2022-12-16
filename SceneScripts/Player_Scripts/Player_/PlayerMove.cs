using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerManagement
{

    


    public PlayerMove(BossController boss, PlayerController player) : base(boss,player)
    {
    }

    public override void OnEnter(PlayerController player)
    {
    }

    public override void OnExit(PlayerController player)
    {

    }

    public override void OnFixedUpdate(PlayerController player)
    {
    }

    public override void OnUpdate(PlayerController player)
    {
        Move(player);

        base.OnUpdate(player);
    }

    private void Move(PlayerController player)
    {
        player.p_anim.SetFloat("Horizontal", h, 0.1f, Time.deltaTime);
        player.p_anim.SetFloat("Vertical", v, 0.1f, Time.deltaTime);
    }

   

}
