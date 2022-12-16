using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboAttack_2 : PlayerManagement
{
    public PlayerComboAttack_2(BossController boss, PlayerController player) : base(boss, player)
    {
    }

    public override void OnEnter(PlayerController player)
    {
        Attack_2(player);

    }

    public override void OnExit(PlayerController player)
    {
        base.OnExit(player);
    }

    public override void OnFixedUpdate(PlayerController player)
    {

    }

    public override void OnUpdate(PlayerController player)
    {
        BladeCollision(player, "Combo_02");
        ComboAtkCheck(player, PlayerController.eState.ComboAttack_3, "Combo_02");
    }

    private void Attack_2(PlayerController player)
    {
        player.p_anim.SetTrigger("Combo_02");
    }

}

