using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFoundation.Code.DebugHelper;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.SettingsSystem
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundEffectsControllerMono : MonoBehaviour, IBilucaLoggable
    {
        [Serializable]
        public class Settings
        {
            [field: SerializeField] public bool PlayOnAwake { get; set; }
            [field: SerializeField] public AudioClip AudioClip { get; set; }
        }

        [SerializeField] private AudioSettingsOptionSO audioSettingsSO;
        [SerializeField] private Settings settings;

        private IAudioSource audioSource;
        private IAudioSettings audioSettings;

        public IBilucaLogger Logger { get; set; }

        public void Awake()
        {
            audioSource = GetComponent<AudioSource>().Decorate();

            if(audioSettingsSO != null)
            {
                audioSettings = audioSettingsSO.Value;
                UpdateSettings();
                audioSettings.OnSettingsChanged += UpdateSettings;
            }
            else
            {
                Logger?.LogHighlight(
                    nameof(SoundEffectsControllerMono), "audio settings was not setup"
                );
            }

            if(settings.PlayOnAwake)
                Play();
        }

        private void UpdateSettings()
        {
            Logger?.LogHighlight(nameof(SoundEffectsControllerMono), "updating");
            audioSource.Volume = audioSettings.Volume;
        }

        private void OnDestroy()
        {
            if(audioSettings != null)
                audioSettings.OnSettingsChanged -= UpdateSettings;
        }

        public void Play()
        {
            Play(settings.AudioClip);
        }

        public void Play(AudioClip audioClip)
        {
            audioSource.Play(audioClip);
        }

        public void Stop()
        {
            audioSource.Stop();
        }

        public void DestroyAfterPlay()
        {
            StartCoroutine(nameof(DestroyAfter));
        }

        private IEnumerator DestroyAfter()
        {
            yield return new WaitForSecondsRealtime(settings.AudioClip.length);
            Destroy(gameObject);
        }
    }
}
