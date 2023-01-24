using System;
using UnityEngine;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.SettingsSystem
{
    public class MusicController : IMusicController
    {
        [Serializable]
        public class Settings
        {
            [field: SerializeField] public AudioClip AudioClip { get; set; }
        }

        public IAudioSource AudioSource { get; }
        private readonly IAudioSettings audioSettings;
        private readonly Settings musicSettings;

        public MusicController(
            IAudioSource audioSource,
            IAudioSettings audioSettings,
            Settings musicSettings
        )
        {
            AudioSource = audioSource;
            this.audioSettings = audioSettings;
            this.musicSettings = musicSettings;

            audioSettings.OnSettingsChanged += HandleSettingsChange;
        }

        private void HandleSettingsChange()
        {
            Debug.Log("HandleSettingsChange");
            AudioSource.Volume = audioSettings.Volume;

            Play();
        }

        public void Play()
        {
            if(audioSettings.Volume == 0 && AudioSource.IsPlaying)
            {
                AudioSource.Stop();
                return;
            }

            if(audioSettings.Volume > 0 && !AudioSource.IsPlaying)
            {
                AudioSource.Play(musicSettings.AudioClip);
            }
        }

        public void Stop()
        {
            audioSettings.OnSettingsChanged -= HandleSettingsChange;
        }
    }
}