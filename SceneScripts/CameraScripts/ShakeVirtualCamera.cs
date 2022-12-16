using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShakeVirtualCamera : MonoBehaviour
{
    private ShakeVirtualCamera() { }
    private static ShakeVirtualCamera instance = null;
    public static ShakeVirtualCamera GetInstance()
    {
        if(instance == null)
        {
            Debug.LogError("������Ʈ�� ���� ���� ����");

        }
        return instance;
    }



    public float shakeDuration = 0f;
    public float shakeAmplitude = 1.2f;
    public float shakeFrequency = 2.0f;


    public CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // ����ī�޶� �����Ѵٸ� ������������ �޾ƿ´�
            virtualCameraNoise = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();

    }

    // ����, ��, �ð��� �Է¹޾Ƽ� �̱������� �����Ѵ�
    public void OnShake(float shakeAmplitude = 1.2f, float shakeFrequency = 2.0f, float shakeDuration = 0f)
    {
        this.shakeAmplitude = shakeAmplitude;
        this.shakeFrequency = shakeFrequency;
        this.shakeDuration = shakeDuration;

        StopCoroutine(ShakeVirtualCam());
        StartCoroutine(ShakeVirtualCam());
    }


    IEnumerator ShakeVirtualCam()
    {
        // cinemachine ������Ʈ�� �غ���� �ʾҴٸ� �������� �ʴ´�
        if (virtualCamera != null || virtualCameraNoise != null)
        {
            // ī�޶� ��鸮�� ���̶��
            while (shakeDuration > 0)
            {
                // �ó׸ӽ��� �������� ���� �������ش�
                virtualCameraNoise.m_AmplitudeGain = shakeAmplitude;
                virtualCameraNoise.m_FrequencyGain = shakeFrequency;

                shakeDuration -= Time.deltaTime;

                yield return null;
            }
            virtualCameraNoise.m_AmplitudeGain = 0f;
            shakeDuration = 0f;

        }

    }

}
