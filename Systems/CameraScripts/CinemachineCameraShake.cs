using Assets.UnityFoundation.Code.Common;
using Assets.UnityFoundation.Code.TimeUtils;
using Cinemachine;
using UnityEngine;

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

        cameraShakeTimer = new Timer(
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
