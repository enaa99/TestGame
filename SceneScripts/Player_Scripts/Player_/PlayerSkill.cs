using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : PlayerManagement
{
    WaitForSeconds wait = new WaitForSeconds(2f);
    WaitForSeconds despawn = new WaitForSeconds(3f);


    public PlayerSkill(BossController boss, PlayerController player) : base(boss, player)
    {
    }

    public override void OnEnter(PlayerController player)
    {
        base.OnEnter(player);
        player.StartCoroutine(DoSkill(player));
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
        CheckAnim(player, "Skill");

    }

    // 스킬 사용시 캐싱된 WaitForSeconds에 따라 각 이펙트 소환 및 회수를 통한 연출
    IEnumerator DoSkill(PlayerController player)
    {
        player.p_anim.SetTrigger("Skill");
        GameObject skillSword = ObjectPoolManager.GetInstance().Spawn("SwordEffect01", player.gameObject);
        skillSword.transform.position = player.transform.position;
        yield return wait;
        GameObject rainSword = ObjectPoolManager.GetInstance().Spawn("SwordEffect02", player.gameObject);
        rainSword.transform.position = player.transform.position;
        yield return despawn;
        ObjectPoolManager.GetInstance().Despawn(skillSword);
        ObjectPoolManager.GetInstance().Despawn(rainSword);

    }






}
