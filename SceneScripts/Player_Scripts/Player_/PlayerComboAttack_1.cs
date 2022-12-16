using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboAttack_1 : PlayerManagement
{
    public PlayerComboAttack_1(BossController boss, PlayerController player) : base(boss, player)
    {
    }

    public override void OnEnter(PlayerController player)
    {
        Attack_1(player);
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
        BladeCollision(player,"Combo_01");
        ComboAtkCheck(player, PlayerController.eState.ComboAttack_2, "Combo_01");
    }

    private void Attack_1(PlayerController player)
    {
        player.p_anim.SetTrigger("Combo_01");
    }

}