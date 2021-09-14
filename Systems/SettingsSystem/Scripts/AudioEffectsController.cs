using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffectsController : Singleton<AudioEffectsController>
{
    [SerializeField] private SettingsOptionSO audioEffectsSettingsOption;
    [SerializeField] private AudioClip clickSoundEffect;

    private AudioSource audioSource;
    private float volume;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        SettingsSaver.Instance.Load(audioEffectsSettingsOption, out volume);

        audioEffectsSettingsOption.OnChangeValue += (sender, args) => {
            SettingsSaver.Instance.Load(audioEffectsSettingsOption, out volume);
            UpdateAudioSourceConfig();
        };

        UpdateAudioSourceConfig();
    }

    private void UpdateAudioSourceConfig()
    {
        audioSource.volume = volume;
    }

    public void PlayClickSound()
    {
        audioSource.clip = clickSoundEffect;
        audioSource.Play();
    }
}
