using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSKillElectric : BossAttackState
{



    public BossSKillElectric(BossController boss, PlayerController player) : base(boss, player)
    {
    }

    public override void OnEnter(BossController boss)
    {
        boss.StartCoroutine(SkillElectricCoroutine(boss));
        boss.StartCoroutine(DespawnElectric(boss));
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

    
    IEnumerator SkillElectricCoroutine(BossController boss)
    {
        // Waitforseconds 캐싱
        WaitForSeconds wait = new WaitForSeconds(spawnTime);

        //원하는 수량만큼의 이펙트 생성
        for (int i = 0; i < boss.spawnCountElectric; i++)
        {
            boss.electricPool_1[i] = ObjectPoolManager.GetInstance().Spawn("Electric", boss.leftPos);
            boss.electricPool_2[i] = ObjectPoolManager.GetInstance().Spawn("Electric", boss.rightPos);
            SetPos(boss.electricPool_1[i], boss.leftPos);
            SetPos(boss.electricPool_2[i], boss.rightPos);

            yield return wait;
        }
        // 생성이 끝나면 다음 행동을 실행
        boss.actNext = true;
    }

    IEnumerator DespawnElectric(BossController boss)
    {
        // Waitforseconds 캐싱
        WaitForSeconds despawnwait = new WaitForSeconds(despawnTime);

        // 일정 시간을 기다린 후 회수 시작
        yield return despawnwait;

        for (int i = 0; i < boss.spawnCountElectric; i++)
        {
            ObjectPoolManager.GetInstance().Despawn(boss.electricPool_1[i]);
            ObjectPoolManager.GetInstance().Despawn(boss.electricPool_2[i]);

        }

    }

    // 이펙트의 위치 설정
    public void SetPos(GameObject skillball, GameObject parent)
    {
        skillball.transform.position = parent.transform.position;

        Quaternion rot = Quaternion.LookRotation((player.transform.position
            + new Vector3(0f, adjustHeight, 0f)) - parent.transform.position);

        parent.transform.rotation = rot;
        skillball.transform.rotation = parent.transform.rotation;
    }

}
