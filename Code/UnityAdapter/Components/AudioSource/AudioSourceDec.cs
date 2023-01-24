using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public class AudioSourceDec : IAudioSource
    {
        private readonly AudioSource audioSource;

        public AudioSourceDec(AudioSource audioSource)
        {
            this.audioSource = audioSource;
        }

        public float Volume {
            get { return audioSource.volume; }
            set { audioSource.volume = Mathf.Clamp01(value); }
        }

        public bool Loop {
            get { return audioSource.loop; }
            set { audioSource.loop = value; }
        }

        public AudioClip AudioClip { 
            get => audioSource.clip; 
            set => audioSource.clip = value; 
        }
        public bool Mute { 
            get => audioSource.mute; 
            set => audioSource.mute = value; 
        }

        public bool IsPlaying => audioSource.isPlaying;

        public void Play(AudioClip clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }

        public void PlayOneShot(AudioClip clip) => audioSource.PlayOneShot(clip);

        public void ResetAudio()
        {
            audioSource.clip = null;
        }

        public void Stop() => audioSource.Stop();
    }
}