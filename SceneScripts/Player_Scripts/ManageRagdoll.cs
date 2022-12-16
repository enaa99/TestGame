using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageRagdoll : MonoBehaviour
{
    private ManageRagdoll() { }
    private static ManageRagdoll instance = null;
    public static ManageRagdoll GetInstance()
    {
        if (instance == null)
        {
            Debug.LogError("오브젝트가 존재 하지 않음");

        }
        return instance;
    }


    public GameObject charObj;
    public GameObject ragdollObj;



    private void Awake()
    {
        instance = this;
    }


    // 플레이어와 렉돌을 교체한다.
    public void ChangeRagdoll(GameObject player, GameObject ragdoll)
    {
        CopyAnimCharacterTransformToRagdoll(player.transform, ragdoll.transform);

        player.gameObject.SetActive(false);
        ragdoll.gameObject.SetActive(true);
    }


    // 플레이어와 렉돌의 포지션을 자식들을 포함시켜 설정해 준다.(rootBone의 위치값까지 받기 위해)
    private void CopyAnimCharacterTransformToRagdoll(Transform origin, Transform rag)
    {
        for (int i = 0; i < origin.transform.childCount; i++)
        {
            if (origin.transform.childCount != 0)
            {
                CopyAnimCharacterTransformToRagdoll(origin.transform.GetChild(i), rag.transform.GetChild(i));
            }
            rag.transform.GetChild(i).localPosition = origin.transform.GetChild(i).localPosition;
            rag.transform.GetChild(i).localRotation = origin.transform.GetChild(i).localRotation;
        }
    }
}

