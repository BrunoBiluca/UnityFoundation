using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.SettingsSystem
{
    public interface IMusicController
    {
        IAudioSource AudioSource { get; }

        void Play();
        void Stop();
    }
}