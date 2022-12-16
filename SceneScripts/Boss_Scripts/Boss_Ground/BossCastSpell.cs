using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCastSpell : BossAttackState
{
    private float spellCastDist = 4f;
    WaitForSeconds wait = new WaitForSeconds(2f);

    public BossCastSpell(BossController boss, PlayerController player) : base(boss, player)
    {
    }


    public override void OnEnter(BossController boss)
    {
        boss.StartCoroutine(CastSpell(boss));
    }

    public override void OnExit(BossController boss)
    {
    }

    public override void OnFixedUpdate(BossController boss)
    {

    }

    public override void OnUpdate(BossController boss)
    {
        boss.CheckAnimation("CastSpell");
        base.OnUpdate(boss);

    }

    IEnumerator CastSpell(BossController boss)
    {
        boss.b_anim.SetTrigger("CastSpell");

        // ObjectPooling을 통해 이펙트 생성 및 원하는 위치로 조정
        GameObject cast = ObjectPoolManager.GetInstance().Spawn("CastSpell", boss.firePos);
        GameObject empact = ObjectPoolManager.GetInstance().Spawn("EmpactSpell", boss.gameObject);
        cast.transform.position = boss.firePos.transform.position + new Vector3(0,0,-spellCastDist);
        empact.transform.position = cast.transform.position;

        // 이펙트 발동 시 캐릭터를 Ragdoll로 교체 연출이용
        ManageRagdoll.GetInstance().ChangeRagdoll(boss.playObj, boss.ragDoll.gameObject);
        ShakeVirtualCamera.GetInstance().OnShake(4f, 2f, 1f);

        // 캐싱한 WaitforSeconds를 이용 일정 시간이 지난 후 이펙트 회수
        yield return wait;
        ObjectPoolManager.GetInstance().Despawn(cast);
        ObjectPoolManager.GetInstance().Despawn(empact);

    }



}
