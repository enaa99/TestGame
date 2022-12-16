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
        //üũ�� �ִϸ��̼� �̸� ����
        boss.curAnimName = "Breath";

        boss.b_anim.SetTrigger("BreathFire");
        //�ִϸ��̼��� ���� ������ ���� true�� �ɶ����� ���
        yield return new WaitUntil(() => boss.AnimName && boss.AnimTime > 0.2f);

        //ObjectPooling�� ���� ����Ʈ �ҷ��� �� position �� rotation ����
        GameObject Breath = ObjectPoolManager.GetInstance().Spawn("Dragon_Flame", boss.firePos);
        boss.breathCollier.enabled = true;
        Breath.transform.position = boss.firePos.transform.position;
        Breath.transform.rotation = boss.firePos.transform.rotation;

        //�ִϸ��̼��� ���� ������ ���� true�� �ɶ����� ���
        yield return new WaitUntil(() => boss.AnimName && boss.AnimTime > 0.9f);
        ObjectPoolManager.GetInstance().Despawn(Breath);
        boss.breathCollier.enabled = false;

    }


}
