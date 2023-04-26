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

            UpdateAudioSourceSettings();
            audioSettings.OnSettingsChanged += HandleSettingsChange;
        }

        private void HandleSettingsChange()
        {
            UpdateAudioSourceSettings();
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
            AudioSource.Stop();
            audioSettings.OnSettingsChanged -= HandleSettingsChange;
        }

        private void UpdateAudioSourceSettings()
        {
            AudioSource.AudioClip = musicSettings.AudioClip;
            AudioSource.Mute = audioSettings.IsMute;
            AudioSource.Volume = audioSettings.Volume;
        }
    }
}