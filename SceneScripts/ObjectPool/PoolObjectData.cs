using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyType = System.String;

[System.Serializable]
public class PoolObjectData
{
    public const int Initial = 10;
    public const int Max = 100;

    public KeyType key;       // Dictionary�� ���� ������ Key ����
    public GameObject prefab; // ������ prefab
    
    public int initialObjectCount = Initial; // ������Ʈ �ʱ� ���� ����
    public int maxObjectCount = Max;     // ť ���� ������ �� �ִ� ������Ʈ �ִ� ����
}