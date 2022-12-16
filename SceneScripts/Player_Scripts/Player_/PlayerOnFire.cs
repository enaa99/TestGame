using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnFire : PlayerManagement
{
    private float onFireTime = 5f;
    

    public PlayerOnFire(BossController boss, PlayerController player) : base(boss, player)
    {

    }

    public override void OnEnter(PlayerController player)
    {
        player.StartCoroutine(OnFireCoroutine(player));
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

    IEnumerator OnFireCoroutine(PlayerController player)
    {
        WaitForSeconds onFire = new WaitForSeconds(onFireTime); //캐싱

        // ObjectPooling을 이용한 이펙트 생성 및 위치 조정
        GameObject fire = ObjectPoolManager.GetInstance().Spawn("OnFire", player.gameObject);
        fire.transform.position = player.transform.position;
        yield return onFire;
        ObjectPoolManager.GetInstance().Despawn(fire);
    }
}
