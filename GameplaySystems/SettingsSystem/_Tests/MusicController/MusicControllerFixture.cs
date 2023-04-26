using Moq;
using UnityEngine;
using UnityFoundation.Code.UnityAdapter;
using UnityFoundation.TestUtility;

namespace UnityFoundation.SettingsSystem.Tests
{
    public class AudioSettingsMockBuilder : MockBuilder<IAudioSettings>
    {
        private float volume = 0f;

        protected override Mock<IAudioSettings> OnBuild()
        {
            var audioSettings = new Mock<IAudioSettings>();
            audioSettings
                .SetupGet(settings => settings.Volume)
                .Returns(() => volume);

            audioSettings
                .SetupSet<float>(s => s.Volume = It.IsAny<float>())
                .Callback(v => {
                    volume = v;
                    audioSettings.Raise(s => s.OnSettingsChanged += null);
                });

            return audioSettings;
        }
    }

    public class MusicControllerFixture : FakeBuilder<IMusicController>, IMusicControllerFixture
    {
        public IAudioSource AudioSource => Get<IAudioSource>();
        public IAudioSettings AudioSettings => Get<IAudioSettings>();

        private bool isPlaying;
        private float audioSourceVolume;

        protected override IMusicController OnBuild()
        {
            Mock<IAudioSource> audioSource = BuildAudioSource();
            AddToObjects(audioSource.Object);

            var audioSettings = new AudioSettingsMockBuilder().Build();
            AddToObjects(audioSettings);

            var musicSettings = new MusicController.Settings();

            return new MusicController(
                audioSource.Object,
                audioSettings,
                musicSettings
            );
        }

        private Mock<IAudioSource> BuildAudioSource()
        {
            var audioSource = new Mock<IAudioSource>();
            audioSource
                .SetupGet(a => a.IsPlaying)
                .Returns(() => isPlaying);
            audioSource
                .Setup(a => a.Play(It.IsAny<AudioClip>()))
                .Callback(() => isPlaying = true);
            audioSource
                .Setup(a => a.Stop())
                .Callback(() => isPlaying = false);
            audioSource
                .SetupSet(a => a.Volume = It.IsAny<float>())
                .Callback<float>(v => audioSourceVolume = v);
            audioSource
                .SetupGet(a => a.Volume)
                .Returns(() => audioSourceVolume);

            return audioSource;
        }
    }
}
