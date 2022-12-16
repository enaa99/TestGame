using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBreathFire : BossAttackState
{
    public BossBreathFire(BossController boss, PlayerController player) : base(boss, player)
    {
    }



    public override void OnEnter(BossController boss)
    {
        boss.StartCoroutine(BreathFireCoroutine(boss));
    }

    public override void OnExit(BossController boss)
    {

    }

    public override void OnFixedUpdate(BossController boss)
    {
      
    }

    public override void OnUpdate(BossController boss)
    {
        boss.CheckAnimation("Breath");
        base.OnUpdate(boss);
    }
    

    
    IEnumerator BreathFireCoroutine(BossController boss)
    {
        //체크할 애니메이션 이름 설정
        boss.curAnimName = "Breath";

        boss.b_anim.SetTrigger("BreathFire");
        //애니메이션의 진행 정도에 따라 true가 될때까지 대기
        yield return new WaitUntil(() => boss.AnimName && boss.AnimTime > 0.2f);

        //ObjectPooling을 통해 이펙트 불러온 후 position 및 rotation 조정
        GameObject Breath = ObjectPoolManager.GetInstance().Spawn("Dragon_Flame", boss.firePos);
        boss.breathCollier.enabled = true;
        Breath.transform.position = boss.firePos.transform.position;
        Breath.transform.rotation = boss.firePos.transform.rotation;

        //애니메이션의 진행 정도에 따라 true가 될때까지 대기
        yield return new WaitUntil(() => boss.AnimName && boss.AnimTime > 0.9f);
        ObjectPoolManager.GetInstance().Despawn(Breath);
        boss.breathCollier.enabled = false;

    }


}
