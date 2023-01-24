using System;

namespace UnityFoundation.SettingsSystem
{
    public interface ISettingsOption
    {
        event Action OnSettingsChanged;
    }
}