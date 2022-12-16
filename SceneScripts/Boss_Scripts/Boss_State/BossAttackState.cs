using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : State<BossController>
{

    public float spawnTime = 0.5f;
    public float despawnTime = 10f;
    public float adjustHeight = 2f;

    private int startSecPhase = 4;
    private int startLastPhase = 8;
    private int afterFinalPhase = 10;
    private int startPhase = 0;
    private int secPhase = 2;
    private int finalPhase = 3;

    public BossAttackState(BossController boss, PlayerController player) : base(boss, player)
    {
    }

    public override void OnEnter(BossController boss)
    {
        boss.CheckDistanceGround();
    }

    public override void OnExit(BossController boss)
    {
    }

    public override void OnFixedUpdate(BossController boss)
    {

    }

    public override void OnUpdate(BossController boss)
    {

        if (!boss.doPattern)
        {
            ChoosePatternPhase(boss);
        }


        if (boss.choosePattern > afterFinalPhase)
        {
            boss.ChangeState(BossController.eState.Dead);
        }
    }

    

    private void ChoosePatternPhase(BossController boss)
    {
   


        if (boss.choosePattern < startSecPhase)
        {
            int rand = Random.Range(startPhase, secPhase);
            boss.pattern.Dopattern(boss, rand);
        }
        else if (boss.choosePattern < startLastPhase)
        {
            boss.pattern.Dopattern(boss, secPhase);
        }
        else
        {
            boss.pattern.Dopattern(boss, finalPhase);
        }

    }

}
