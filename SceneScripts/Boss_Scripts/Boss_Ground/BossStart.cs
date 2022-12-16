using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStart : State<BossController>
{
    public BossStart(BossController boss, PlayerController player) : base(boss, player)
    {
    }

    public override void OnEnter(BossController boss)
    {
       
    }

    public override void OnExit(BossController boss)
    {

    }

    public override void OnFixedUpdate(BossController boss)
    {

    }

    public override void OnUpdate(BossController boss)
    {
        //�ִϸ��̼� ���� ������ ���� VirtualCamera����
        if(boss.AnimTime > 0.6f)
        {
            boss.startCam.Priority = 1;
        }

        // �ִϸ��̼� ���� ������ ���� ������Ʈ ��ȯ
        if(boss.AnimTime > 0.9f)
        {
            boss.ChangeState(BossController.eState.Wander);
        }

    }

}
