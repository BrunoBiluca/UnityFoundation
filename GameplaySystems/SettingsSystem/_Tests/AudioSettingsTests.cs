using Moq;
using NUnit.Framework;

namespace UnityFoundation.SettingsSystem.Tests
{
    public class AudioSettingsTests
    {
        [Test]
        public void Should_save_settings_when_volume_changes()
        {
            var saver = new Mock<ISettingsSaver<AudioSettings>>();
            var audioSettings = new AudioSettings(saver.Object) {
                Volume = .5f
            };

            saver.Verify(s => s.Save(It.IsAny<AudioSettings>()), Times.Once());
        }

        [Test]
        public void Should_save_settings_when_is_mute_changes()
        {
            var saver = new Mock<ISettingsSaver<AudioSettings>>();
            var audioSettings = new AudioSettings(saver.Object) {
                IsMute = true
            };

            saver.Verify(s => s.Save(It.IsAny<AudioSettings>()), Times.Once());
        }
    }
}
