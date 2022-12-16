using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyType = System.String;

public class ObjectPoolManager : MonoBehaviour
{
    // 싱글톤
    private ObjectPoolManager() { }
    private static ObjectPoolManager instance = null;
    public static ObjectPoolManager GetInstance()
    {
        if(instance == null)
        {
            Debug.LogError("오브젝트가 존재 하지 않음");
        }
        return instance;
    }


    [SerializeField]
    private List<PoolObjectData> _poolObjectDataList = new List<PoolObjectData>(4);

    private Dictionary<KeyType, GameObject> _sampleDict;              // Key - 복제용 오브젝트 원본
    private Dictionary<KeyType, PoolObjectData> _dataDict;            // Key - 풀 정보
    private Dictionary<KeyType, Stack<GameObject>> _poolDict;         // Key - 풀
    private Dictionary<GameObject, Stack<GameObject>> _clonePoolDict; // 복제된 게임오브젝트 - 풀

    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        Init();
        
    }

    private void Init()
    {
        int len = _poolObjectDataList.Count;
        if (len == 0) return;

        // Dictionary 생성
        _sampleDict = new Dictionary<KeyType, GameObject>(len);
        _dataDict = new Dictionary<KeyType, PoolObjectData>(len);
        _poolDict = new Dictionary<KeyType, Stack<GameObject>>(len);
        _clonePoolDict = new Dictionary<GameObject, Stack<GameObject>>(len * PoolObjectData.Max);

        // Data로부터 새로운 Pool 오브젝트 정보 생성
        foreach (var data in _poolObjectDataList)
        {
            RegisterInternal(data);
        }
    }

    /// Pool 데이터로부터 새로운 Pool 오브젝트 정보 등록 
    private void RegisterInternal(PoolObjectData data)
    {
        // 중복 키는 등록 불가능
        if (_poolDict.ContainsKey(data.key))
        {
            return;
        }

        // 샘플 게임오브젝트 생성
        GameObject sample = Instantiate(data.prefab);
        sample.name = data.prefab.name;
        sample.SetActive(false);
        sample.transform.SetParent(this.transform);
         

        //  Pool Dictionary에 풀 생성 및 풀에 미리 오브젝트들 만들어 담아놓기
        Stack<GameObject> pool = new Stack<GameObject>(data.maxObjectCount);
        for (int i = 0; i < data.initialObjectCount; i++)
        {
            GameObject clone = Instantiate(data.prefab);
            clone.SetActive(false);
            pool.Push(clone);
            clone.transform.SetParent(this.transform);

            _clonePoolDict.Add(clone, pool); // Clone-Stack 캐싱

        }

        // 딕셔너리 추가
        _sampleDict.Add(data.key, sample);
        _dataDict.Add(data.key, data);
        _poolDict.Add(data.key, pool);           
    }

    /// 샘플 오브젝트 복제하기
    private GameObject CloneSample(KeyType key)
    {
        if (!_sampleDict.TryGetValue(key, out GameObject sample)) return null;

        return Instantiate(sample);
    }

    ///  풀에서 꺼내오기 
    public GameObject Spawn(KeyType key,GameObject gameObject)
    {
        // 키가 존재하지 않는 경우 null 리턴
        if (!_poolDict.TryGetValue(key, out var pool))
        {
            return null;
        }

        GameObject go;

        //  풀에 재고가 있는 경우 
        if (pool.Count > 0)
        {
            go = pool.Pop();  
        }
        //  재고가 없는 경우 샘플로부터 복제
        else
        {
            go = CloneSample(key);
            _clonePoolDict.Add(go, pool); // Clone-Stack 캐싱
        }

        go.SetActive(true);
        go.transform.SetParent(gameObject.transform);
 

        return go;
    }

    ///  풀에 집어넣기 
    public void Despawn(GameObject go)
    {
        // 캐싱된 게임오브젝트가 아닌 경우 파괴 
        if (!_clonePoolDict.TryGetValue(go, out var pool))
        {
            Destroy(go);
            return;
        }

        // 집어넣기
        go.SetActive(false);
        go.transform.SetParent(this.transform);
        pool.Push(go);
    }
}