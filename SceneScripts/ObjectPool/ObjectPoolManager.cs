using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyType = System.String;

public class ObjectPoolManager : MonoBehaviour
{
    // �̱���
    private ObjectPoolManager() { }
    private static ObjectPoolManager instance = null;
    public static ObjectPoolManager GetInstance()
    {
        if(instance == null)
        {
            Debug.LogError("������Ʈ�� ���� ���� ����");
        }
        return instance;
    }


    [SerializeField]
    private List<PoolObjectData> _poolObjectDataList = new List<PoolObjectData>(4);

    private Dictionary<KeyType, GameObject> _sampleDict;              // Key - ������ ������Ʈ ����
    private Dictionary<KeyType, PoolObjectData> _dataDict;            // Key - Ǯ ����
    private Dictionary<KeyType, Stack<GameObject>> _poolDict;         // Key - Ǯ
    private Dictionary<GameObject, Stack<GameObject>> _clonePoolDict; // ������ ���ӿ�����Ʈ - Ǯ

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

        // Dictionary ����
        _sampleDict = new Dictionary<KeyType, GameObject>(len);
        _dataDict = new Dictionary<KeyType, PoolObjectData>(len);
        _poolDict = new Dictionary<KeyType, Stack<GameObject>>(len);
        _clonePoolDict = new Dictionary<GameObject, Stack<GameObject>>(len * PoolObjectData.Max);

        // Data�κ��� ���ο� Pool ������Ʈ ���� ����
        foreach (var data in _poolObjectDataList)
        {
            RegisterInternal(data);
        }
    }

    /// Pool �����ͷκ��� ���ο� Pool ������Ʈ ���� ��� 
    private void RegisterInternal(PoolObjectData data)
    {
        // �ߺ� Ű�� ��� �Ұ���
        if (_poolDict.ContainsKey(data.key))
        {
            return;
        }

        // ���� ���ӿ�����Ʈ ����
        GameObject sample = Instantiate(data.prefab);
        sample.name = data.prefab.name;
        sample.SetActive(false);
        sample.transform.SetParent(this.transform);
         

        //  Pool Dictionary�� Ǯ ���� �� Ǯ�� �̸� ������Ʈ�� ����� ��Ƴ���
        Stack<GameObject> pool = new Stack<GameObject>(data.maxObjectCount);
        for (int i = 0; i < data.initialObjectCount; i++)
        {
            GameObject clone = Instantiate(data.prefab);
            clone.SetActive(false);
            pool.Push(clone);
            clone.transform.SetParent(this.transform);

            _clonePoolDict.Add(clone, pool); // Clone-Stack ĳ��

        }

        // ��ųʸ� �߰�
        _sampleDict.Add(data.key, sample);
        _dataDict.Add(data.key, data);
        _poolDict.Add(data.key, pool);           
    }

    /// ���� ������Ʈ �����ϱ�
    private GameObject CloneSample(KeyType key)
    {
        if (!_sampleDict.TryGetValue(key, out GameObject sample)) return null;

        return Instantiate(sample);
    }

    ///  Ǯ���� �������� 
    public GameObject Spawn(KeyType key,GameObject gameObject)
    {
        // Ű�� �������� �ʴ� ��� null ����
        if (!_poolDict.TryGetValue(key, out var pool))
        {
            return null;
        }

        GameObject go;

        //  Ǯ�� ��� �ִ� ��� 
        if (pool.Count > 0)
        {
            go = pool.Pop();  
        }
        //  ��� ���� ��� ���÷κ��� ����
        else
        {
            go = CloneSample(key);
            _clonePoolDict.Add(go, pool); // Clone-Stack ĳ��
        }

        go.SetActive(true);
        go.transform.SetParent(gameObject.transform);
 

        return go;
    }

    ///  Ǯ�� ����ֱ� 
    public void Despawn(GameObject go)
    {
        // ĳ�̵� ���ӿ�����Ʈ�� �ƴ� ��� �ı� 
        if (!_clonePoolDict.TryGetValue(go, out var pool))
        {
            Destroy(go);
            return;
        }

        // ����ֱ�
        go.SetActive(false);
        go.transform.SetParent(this.transform);
        pool.Push(go);
    }
}