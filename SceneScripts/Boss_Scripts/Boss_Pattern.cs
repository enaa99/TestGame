using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Pattern : MonoBehaviour
{
    [SerializeField]
    private List<Boss_Patterndata> boss_Patterns = new List<Boss_Patterndata>(4);

    private Boss_Patterndata boss_patterndata;
    private Dictionary<Boss_Patterndata.Sequence, Boss_Patterndata> _keyDict;   // ���� ����

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (boss_Patterns.Count == 0) return;

        // Dicitionary ����

        _keyDict  =  new Dictionary<Boss_Patterndata.Sequence, Boss_Patterndata>();

        foreach(var data in boss_Patterns)
        {
            RegisterPattern(data);
        }

    }

    private void RegisterPattern(Boss_Patterndata data)
    {
        // �ߺ� ���� ��� �Ұ���
        if (_keyDict.ContainsKey(data.key)) return;
    
        // ��ųʸ� �߰�
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
        // �ൿ ���� ��ȯ
        int idx = 0;
        
        // Key�� �������� �ʴ� ��� ����
        if (!_keyDict.TryGetValue(key.key, out var data)) return ;

        // list�� ���� ���� ���� ��� ����
        if (data.act.Count < idx) return;


        StartCoroutine(DoActCoroutine(data,boss));
    }

    // �ൿ �ڷ�ƾ
    IEnumerator DoActCoroutine(Boss_Patterndata data, BossController boss)
    {
        int idx = 0;

        while (data.act.Count > idx)
        {
            // �Էµ� �ൿ�� ������� ����

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
        //������ �����͸� ������Ʈ�ѷ��� ���� ����

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
