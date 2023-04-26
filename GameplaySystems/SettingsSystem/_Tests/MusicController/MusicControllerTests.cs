using NUnit.Framework;

namespace UnityFoundation.SettingsSystem.Tests
{

    [TestFixture(typeof(MusicControllerFixture), TestName = "Testing Music Controller")]
    public class MusicControllerTests<T> where T : IMusicControllerFixture, new()
    {
        private T fixture;
        IMusicController musicController;

        [SetUp]
        public void Setup()
        {
            fixture = new T();
            musicController = fixture.Build();
        }

        [Test]
        public void Should_not_play_music_when_audio_settings_has_no_volume()
        {
            musicController.Play();

            Assert.That(musicController.AudioSource.IsPlaying, Is.False);
        }

        [Test]
        public void Should_play_music_when_audio_settings_has_volume()
        {
            fixture.AudioSettings.Volume = 1f;

            musicController.Play();

            Assert.That(musicController.AudioSource.IsPlaying, Is.True);
        }

        [Test]
        public void Should_update_controller_when_audio_settings_changes()
        {
            fixture.AudioSettings.Volume = 1f;

            Assert.That(musicController.AudioSource.Volume, Is.EqualTo(1f));

            fixture.AudioSettings.Volume = .5f;

            Assert.That(musicController.AudioSource.Volume, Is.EqualTo(.5f));
        }

        [Test]
        public void Should_stop_music_when_audio_change_to_zero()
        {
            fixture.AudioSettings.Volume = 1f;

            musicController.Play();

            Assert.That(musicController.AudioSource.Volume, Is.EqualTo(1f));
            Assert.That(musicController.AudioSource.IsPlaying, Is.True);

            fixture.AudioSettings.Volume = 0f;

            Assert.That(musicController.AudioSource.Volume, Is.EqualTo(0f));
            Assert.That(musicController.AudioSource.IsPlaying, Is.False);
            
            fixture.AudioSettings.Volume = .5f;

            Assert.That(musicController.AudioSource.Volume, Is.EqualTo(.5f));
            Assert.That(musicController.AudioSource.IsPlaying, Is.True);
        }
    }
}
