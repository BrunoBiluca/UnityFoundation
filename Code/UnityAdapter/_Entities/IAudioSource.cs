using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public interface IAudioSource
    {
        float Volume { get; set; }
        bool Loop { get; set; }

        void Play(AudioClip clip);
        void PlayOneShot(AudioClip clip);
        void ResetAudio();
        void Stop();
    }
}