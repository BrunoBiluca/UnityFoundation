using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public interface IAudioSource
    {
        AudioClip AudioClip { get; set; }
        float Volume { get; set; }
        bool Loop { get; set; }
        bool Mute { get; set; }
        bool IsPlaying { get; }

        void Play(AudioClip clip);
        void PlayOneShot(AudioClip clip);
        void ResetAudio();
        void Stop();
    }
}