using Assets.UnityFoundation.UnityAdapter;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceDec : IAudioSource
{
    private readonly AudioSource audioSource;

    public AudioSourceDec(AudioSource audioSource)
    {
        this.audioSource = audioSource;
    }

    public void PlayOneShot(AudioClip clip) => audioSource.PlayOneShot(clip);
}
