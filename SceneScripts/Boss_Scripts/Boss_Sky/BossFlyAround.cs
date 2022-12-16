using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFlyAround : BossSkyState
{
    private float minY = 0f;
    private float maxY = 15f;
    private float flyHeight = 6f;

    WaitForSeconds explosionWait = new WaitForSeconds(1f); 
    WaitForSeconds aroundTime = new WaitForSeconds(10f);   
    WaitForSeconds despawnTime = new WaitForSeconds(40f);  


    public BossFlyAround(BossController boss, PlayerController player) : base(boss, player)
    {
    }

    public override void OnEnter(BossController boss)
    {

        FlyMove(boss);
        boss.StartCoroutine(StartExplosion(boss));
        boss.StartCoroutine(DespawnExplosion(boss));

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
  
    private void FlyMove(BossController boss)
    {
        boss.b_rigid.useGravity = false;
        boss.b_anim.SetTrigger("FlyAround");
        boss.transform.position = new Vector3(boss.transform.position.x, flyHeight, boss.transform.position.z);

    }

    
    IEnumerator StartExplosion(BossController boss)
    {
        for (int j = 0; j < boss.explosionCount; j++)
        {
            // 다중배열을 이용 원하는 이펙트들을 지정한 위치로 생성 
            for (int i = 0; i < boss.spawnCountExplosion; i++)
            {
                boss.explosion[j, i] = ObjectPoolManager.GetInstance().Spawn($"Explosion{j}", boss.explosionCollider.gameObject);
                boss.explosion[j, i].transform.position = Return_RandonPosition(boss);

            }
            yield return explosionWait;
        }
        
        yield return aroundTime;
        boss.actNext = true;

    }

    IEnumerator DespawnExplosion(BossController boss)
    {
        yield return despawnTime;

        for (int j = 0; j < boss.explosionCount; j++)
        {
            for (int i = 0; i < boss.spawnCountExplosion; i++)
            {
                // 다중 배열을 이용 이펙트 회수
                ObjectPoolManager.GetInstance().Despawn(boss.explosion[j,i]);

                yield return null;
            }
        }
    }


    // 랜덤한 위치를 Return하는 함수
    Vector3 Return_RandonPosition(BossController boss)
    {
        Vector3 originPos = player.transform.position;

        float range_X = boss.explosionCollider.bounds.size.x;
        float range_Z = boss.explosionCollider.bounds.size.z;
        float range_Y;

        range_X = Random.Range((range_X * 0.5f) * -1f, range_X * 0.5f);
        range_Z = Random.Range((range_Z * 0.5f) * -1f, range_Z * 0.5f);
        range_Y = Random.Range(minY, maxY);

        Vector3 RandomPosition = new Vector3(range_X, range_Y, range_Z);

        Vector3 respawnPosition = originPos + RandomPosition;
        return respawnPosition;
    }

}