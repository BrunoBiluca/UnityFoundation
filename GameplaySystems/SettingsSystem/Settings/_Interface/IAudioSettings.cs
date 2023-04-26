using System;

namespace UnityFoundation.SettingsSystem
{
    public interface IAudioSettings : ISettingsOption
    {
        float Volume { get; set; }
        bool IsMute { get; set; }
        Meta MetaSettings { get; }

        [Serializable]
        public class Meta
        {
            public float MinValue { get; set; } = 0f;
            public float MaxValue { get; set; } = 1f;
        }
    }
}