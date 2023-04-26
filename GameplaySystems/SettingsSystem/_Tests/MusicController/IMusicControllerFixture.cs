using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.SettingsSystem.Tests
{
    public interface IMusicControllerFixture
    {
        IAudioSource AudioSource { get; }
        IAudioSettings AudioSettings { get; }

        IMusicController Build();
    }
}
