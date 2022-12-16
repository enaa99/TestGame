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
            Debug.LogError("������Ʈ�� ���� ���� ����");

        }
        return instance;
    }


    public GameObject charObj;
    public GameObject ragdollObj;



    private void Awake()
    {
        instance = this;
    }


    // �÷��̾�� ������ ��ü�Ѵ�.
    public void ChangeRagdoll(GameObject player, GameObject ragdoll)
    {
        CopyAnimCharacterTransformToRagdoll(player.transform, ragdoll.transform);

        player.gameObject.SetActive(false);
        ragdoll.gameObject.SetActive(true);
    }


    // �÷��̾�� ������ �������� �ڽĵ��� ���Խ��� ������ �ش�.(rootBone�� ��ġ������ �ޱ� ����)
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

