using System;
using UnityEngine;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.SettingsSystem
{

    [Serializable]
    [CreateAssetMenu(
        menuName = UnityFoundationEditorConfig.BASE_CONTEXT_MENU_PATH + "Settings/Audio Settings"
    )]
    public class AudioSettingsOptionSO : SettingsOptionSO<AudioSettings>
    {
        protected override AudioSettings Instantiate()
        {
            return new AudioSettings(this);
        }
    }
}