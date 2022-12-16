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
        // üũ �ϰ� ���� animation�̸� ����
        boss.curAnimName = "FireAround";
        boss.b_anim.SetTrigger(boss.curAnimName);

        // �ִϸ��̼��� ���� ������ ���� true�� �� ����Ʈ ���� �� ��ġ ���� 
        yield return new WaitUntil(() => boss.AnimName && boss.AnimTime > 0.2f);
        GameObject BreathAround = ObjectPoolManager.GetInstance().Spawn("Dragon_Flame", boss.firePos);
        boss.breathCollier.enabled = true;
        BreathAround.transform.position = boss.firePos.transform.position;
        BreathAround.transform.rotation = boss.firePos.transform.rotation;
        
        // �ִϸ��̼��� ���� ������ ���� ����Ʈ ȸ�� 
        yield return new WaitUntil(() => boss.AnimName && boss.AnimTime > 0.9f);
        ObjectPoolManager.GetInstance().Despawn(BreathAround);
        boss.breathCollier.enabled = false;

    }



}
