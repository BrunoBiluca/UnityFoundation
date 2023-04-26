using System;

namespace UnityFoundation.SettingsSystem
{
    [Serializable]
    public sealed class AudioSettings : IAudioSettings
    {
        private readonly ISettingsSaver<AudioSettings> saver;

        private float volume;
        public float Volume {
            get { return volume; }
            set {
                volume = value;
                OnSettingsChanged?.Invoke();
            }
        }

        private bool isMute;
        public bool IsMute {
            get { return isMute; }
            set {
                isMute = value;
                OnSettingsChanged?.Invoke();
            }
        }

        public IAudioSettings.Meta MetaSettings { get; private set; }

        public event Action OnSettingsChanged;

        public AudioSettings()
        {
            MetaSettings = new IAudioSettings.Meta();
        }

        public AudioSettings(ISettingsSaver<AudioSettings> saver) : this()
        {
            this.saver = saver;
            OnSettingsChanged += SaveSettings;
        }

        private void SaveSettings()
        {
            saver.Save(this);
        }
    }
}
