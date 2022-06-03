using UnityFoundation.Code;
using Cinemachine;
using UnityEngine;
using UnityFoundation.Tools.TimeUtils;

public class CinemachineCameraShake : Singleton<CinemachineCameraShake>
{
    [SerializeField] private float shakeTimeAmount = 1f;
    [SerializeField] private CinemachineVirtualCamera vCamera;

    private CinemachineBasicMultiChannelPerlin channel;
    private Timer cameraShakeTimer;

    protected override void OnAwake()
    {
        if(vCamera == null)
            vCamera = GetComponent<CinemachineVirtualCamera>();

        channel = vCamera
            .GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cameraShakeTimer = (Timer)new Timer(
            shakeTimeAmount,
            () => channel.m_AmplitudeGain = 0f
        )
            .SetName("camera_shake")
            .RunOnce();
    }

    public void Shake()
    {
        channel.m_AmplitudeGain = 5f;
        cameraShakeTimer.Start();
    }
}
