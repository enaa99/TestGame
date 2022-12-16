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

        // ObjectPooling�� ���� ����Ʈ ���� �� ���ϴ� ��ġ�� ����
        GameObject cast = ObjectPoolManager.GetInstance().Spawn("CastSpell", boss.firePos);
        GameObject empact = ObjectPoolManager.GetInstance().Spawn("EmpactSpell", boss.gameObject);
        cast.transform.position = boss.firePos.transform.position + new Vector3(0,0,-spellCastDist);
        empact.transform.position = cast.transform.position;

        // ����Ʈ �ߵ� �� ĳ���͸� Ragdoll�� ��ü �����̿�
        ManageRagdoll.GetInstance().ChangeRagdoll(boss.playObj, boss.ragDoll.gameObject);
        ShakeVirtualCamera.GetInstance().OnShake(4f, 2f, 1f);

        // ĳ���� WaitforSeconds�� �̿� ���� �ð��� ���� �� ����Ʈ ȸ��
        yield return wait;
        ObjectPoolManager.GetInstance().Despawn(cast);
        ObjectPoolManager.GetInstance().Despawn(empact);

    }



}
