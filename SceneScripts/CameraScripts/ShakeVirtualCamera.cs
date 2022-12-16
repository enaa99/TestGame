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
            Debug.LogError("오브젝트가 존재 하지 않음");

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
        // 가상카메라가 존재한다면 노이즈정보를 받아온다
            virtualCameraNoise = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();

    }

    // 강도, 빈도, 시간을 입력받아서 싱글톤으로 실행한다
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
        // cinemachine 컴포넌트가 준비되지 않았다면 실행하지 않는다
        if (virtualCamera != null || virtualCameraNoise != null)
        {
            // 카메라가 흔들리는 중이라면
            while (shakeDuration > 0)
            {
                // 시네머신의 노이즈의 값을 설정해준다
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
