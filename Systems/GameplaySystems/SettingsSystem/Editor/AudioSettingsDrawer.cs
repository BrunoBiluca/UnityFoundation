using UnityEditor;
using UnityEngine;
using UnityFoundation.Code;
using UnityFoundation.Editor;

namespace UnityFoundation.SettingsSystem.Editor
{

    [CustomPropertyDrawer(typeof(AudioSettings))]
    public class AudioSettingsDrawer : PropertyDrawer
    {
        private PropertyBuilder propertyBuilder;
        private bool isFoldout = false;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if(propertyBuilder == null)
                return base.GetPropertyHeight(property, label);

            return propertyBuilder.Height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var audio = property.GetField<AudioSettings>();
            propertyBuilder = new PropertyBuilder(position) { ItemSize = 20f };

            propertyBuilder.Toggle("IsMute", audio.IsMute, (v) => audio.IsMute = v);
            propertyBuilder.FloatField("Volume", audio.Volume, (v) => audio.Volume = v);

            var group = propertyBuilder.FoldoutGroud("Meta", isFoldout, (v) => isFoldout = v);
            group.Add(() =>
                propertyBuilder.FloatField(
                    "MinValue",
                    audio.MetaSettings.MinValue,
                    (v) => audio.MetaSettings.MinValue = v
                )
            );
            group.Add(() =>
                propertyBuilder.FloatField(
                    "MaxValue",
                    audio.MetaSettings.MaxValue,
                    (v) => audio.MetaSettings.MaxValue = v
                )
            );
            group.End();
        }
    }
}
