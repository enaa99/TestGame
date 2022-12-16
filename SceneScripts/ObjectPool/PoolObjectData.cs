using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyType = System.String;

[System.Serializable]
public class PoolObjectData
{
    public const int Initial = 10;
    public const int Max = 100;

    public KeyType key;       // Dictionary를 통해 접근할 Key 설정
    public GameObject prefab; // 복제할 prefab
    
    public int initialObjectCount = Initial; // 오브젝트 초기 생성 개수
    public int maxObjectCount = Max;     // 큐 내에 보관할 수 있는 오브젝트 최대 개수
}