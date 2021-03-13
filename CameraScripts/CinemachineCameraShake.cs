using Assets.UnityFoundation.TimeUtils;
using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CinemachineCameraShake : MonoBehaviour {

    public static CinemachineCameraShake Instance { get; private set; }

    public CinemachineVirtualCamera virtualCamera;

    private Timer cameraShakeTimer;

    public void Awake() {
        Instance = this;
    }

    public void Shake() {
        var channel = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        channel.m_AmplitudeGain = 5f;

        if(cameraShakeTimer == null) {
            cameraShakeTimer = new Timer(
                "camera_shake", 
                1f, 
                () => {
                    channel.m_AmplitudeGain = 0f;
                },
                true
            );
        }
        else {
            cameraShakeTimer.Restart();
        }
    }
}
