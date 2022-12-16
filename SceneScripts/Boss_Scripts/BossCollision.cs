using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCollision : MonoBehaviour
{
    public BossController boss;
    public SkinnedMeshRenderer[] skinnedMeshRenderers;

    WaitForSeconds wait = new WaitForSeconds(0.01f);
    WaitForSeconds moment = new WaitForSeconds(0.1f);
    WaitForSeconds hit = new WaitForSeconds(0.5f);


    [ColorUsageAttribute(true, true)]
    public Color shouldbeHDR = Color.white;

    private Color[] originColor;
    private int i = 0;

    private void Awake()
    {
        originColor = new Color[skinnedMeshRenderers.Length];
    }


    private void Start()
    {
        // 기존 Color 설정
        for (int i = 0; i < skinnedMeshRenderers.Length; i++)
        {
            originColor[i] = skinnedMeshRenderers[i].material.color;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Blade") && !boss.IsState(BossController.eState.Dead))
        {
            StartCoroutine(OnhitColor());
            StartCoroutine(SetTimeScale());
            StartCoroutine(OnHitEffect(other));
            ShakeVirtualCamera.GetInstance().OnShake(2f, 2f, 0.3f);
            boss.choosePattern++;
        }
    }

    // TimeScale을 통한 연출
    IEnumerator SetTimeScale()
    {
        Time.timeScale = 0.2f;
        yield return moment;
        Time.timeScale = 1;
    }

    //피격시 생성되는 이펙트 설정 및 회수
    private IEnumerator OnHitEffect(Collider collider)
    {
        GameObject hits = ObjectPoolManager.GetInstance().Spawn("HitEffect", this.gameObject);
        hits.transform.position = collider.transform.position + new Vector3(0f,1f,0f);
        yield return hit;
        ObjectPoolManager.GetInstance().Despawn(hits);
    }

    // SkinnedMeshRenderer 의 색상을 지정한 색상으로 바꾼 후 moment만큼 기다린 후 원상복구
    IEnumerator OnhitColor()
    {
        ChangeColor(shouldbeHDR);

        yield return moment;

        for (int i = 0; i < skinnedMeshRenderers.Length; i++)
        {
            skinnedMeshRenderers[i].material.color = originColor[i];
        }

    }

    private void ChangeColor(Color color)
    {
        for (i = 0; i < skinnedMeshRenderers.Length; i++)
        {
            skinnedMeshRenderers[i].material.color = color;
        }
  
    }





}
