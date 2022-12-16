using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Pattern : MonoBehaviour
{
    [SerializeField]
    private List<Boss_Patterndata> boss_Patterns = new List<Boss_Patterndata>(4);

    private Boss_Patterndata boss_patterndata;
    private Dictionary<Boss_Patterndata.Sequence, Boss_Patterndata> _keyDict;   // 패턴 정보

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (boss_Patterns.Count == 0) return;

        // Dicitionary 생성

        _keyDict  =  new Dictionary<Boss_Patterndata.Sequence, Boss_Patterndata>();

        foreach(var data in boss_Patterns)
        {
            RegisterPattern(data);
        }

    }

    private void RegisterPattern(Boss_Patterndata data)
    {
        // 중복 상태 사용 불가능
        if (_keyDict.ContainsKey(data.key)) return;
    
        // 딕셔너리 추가
       _keyDict.Add(data.key, data);
    
    }

    public void Dopattern(BossController boss, int chosen)
    {
        boss.doPattern = true;
        boss_patterndata = ChangePattern(chosen);
        DoAct(boss_patterndata, boss);

    }


    public void DoAct(Boss_Patterndata key, BossController boss)
    {
        // 행동 상태 전환
        int idx = 0;
        
        // Key가 존재하지 않는 경우 리턴
        if (!_keyDict.TryGetValue(key.key, out var data)) return ;

        // list가 존재 하지 않을 경우 리턴
        if (data.act.Count < idx) return;


        StartCoroutine(DoActCoroutine(data,boss));
    }

    // 행동 코로틴
    IEnumerator DoActCoroutine(Boss_Patterndata data, BossController boss)
    {
        int idx = 0;

        while (data.act.Count > idx)
        {
            // 입력된 행동을 순서대로 실행

            boss.actNext = false;
            ChangeState(data, boss, idx++);
            yield return new WaitUntil(() => boss.actNext == true);
          
        }

        boss.doPattern = false;
        
    }

    private void ChangeState(Boss_Patterndata data, BossController boss,int sequence)
    {

        boss.ChangeState(data.act[sequence]);

    }

    IEnumerator PatternCoolTime(Boss_Patterndata data)
    {
        data.doNext = false;
        float timer = 0;
        while(data.coolTime >=timer)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        data.doNext = true;
    }


    private Boss_Patterndata ChangePattern(int chosen)
    {
        //패턴의 데이터를 보스컨트롤러를 통해 받음

        boss_patterndata = boss_Patterns[chosen];

        if (boss_patterndata.doNext)
        {
            StartCoroutine(PatternCoolTime(boss_patterndata));
            return boss_patterndata;
        }
        else
        {
            boss_patterndata = boss_Patterns[0];  
            return boss_patterndata;
        }
        
    }

}
