using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireAround : BossAttackState
{

    public BossFireAround(BossController boss, PlayerController player) : base(boss, player)
    {

    }



    public override void OnEnter(BossController boss)
    {
        boss.StartCoroutine(FireAround(boss));
    }

    public override void OnExit(BossController boss)
    {
    }

    public override void OnFixedUpdate(BossController boss)
    {

    }

    public override void OnUpdate(BossController boss)
    {
        boss.CheckAnimation("FireAround");
        base.OnUpdate(boss);

    }

    IEnumerator FireAround(BossController boss)
    {
        // 체크 하고 싶은 animation이름 설정
        boss.curAnimName = "FireAround";
        boss.b_anim.SetTrigger(boss.curAnimName);

        // 애니메이션의 진행 정도에 따라 true일 시 이펙트 생성 및 위치 조정 
        yield return new WaitUntil(() => boss.AnimName && boss.AnimTime > 0.2f);
        GameObject BreathAround = ObjectPoolManager.GetInstance().Spawn("Dragon_Flame", boss.firePos);
        boss.breathCollier.enabled = true;
        BreathAround.transform.position = boss.firePos.transform.position;
        BreathAround.transform.rotation = boss.firePos.transform.rotation;
        
        // 애니메이션의 진행 정도에 따라 이펙트 회수 
        yield return new WaitUntil(() => boss.AnimName && boss.AnimTime > 0.9f);
        ObjectPoolManager.GetInstance().Despawn(BreathAround);
        boss.breathCollier.enabled = false;

    }



}
