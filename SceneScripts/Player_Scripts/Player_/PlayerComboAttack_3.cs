using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboAttack_3 : PlayerManagement
{
    public PlayerComboAttack_3(BossController boss, PlayerController player) : base(boss, player)
    {
    }

    public override void OnEnter(PlayerController player)
    {
        Attack_3(player);
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
        BladeCollision(player, "Combo_03");
        ComboAtkCheck(player, PlayerController.eState.ComboAttack_1, "Combo_03");
    }

    private void Attack_3(PlayerController player)
    {
        player.p_anim.SetTrigger("Combo_03");
    }
}
