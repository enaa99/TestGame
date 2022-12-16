using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFlyUp : BossSkyState
{
    private float upWard = 1.2f;
    WaitForSeconds wait = new WaitForSeconds(10f);
    GameObject smoke;

    public BossFlyUp(BossController boss, PlayerController player) : base(boss, player)
    {
    }

    public override void OnEnter(BossController boss)
    {
        boss.StartCoroutine(FlyUpCoroutine(boss));
        boss.StartCoroutine(DespawnSmoke(boss));
    }

    public override void OnExit(BossController boss)
    {

    }

    public override void OnFixedUpdate(BossController boss)
    {

    }

    public override void OnUpdate(BossController boss)
    {
        base.OnUpdate(boss);
    }

    IEnumerator FlyUpCoroutine(BossController boss)
    {
        //캐싱
        WaitForSeconds flyBack = new WaitForSeconds(upWard);

        // ObjectPooling을 이용 이펙트 생성 및 위치조정
        smoke = ObjectPoolManager.GetInstance().Spawn("FlySmoke", boss.gameObject);
        smoke.transform.position = boss.transform.position;
        boss.landPos = boss.transform.position;
        boss.b_anim.SetTrigger("FlyUp");
        yield return flyBack;
        boss.actNext = true;
    }

    IEnumerator DespawnSmoke(BossController boss)
    {
        yield return wait;
        ObjectPoolManager.GetInstance().Despawn(smoke);
    }



}


