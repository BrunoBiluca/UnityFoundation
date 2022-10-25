using UnityEditor;
using UnityEngine;
using UnityFoundation.Code;

namespace UnityFoundation.Editor
{
    [CustomPropertyDrawer(typeof(Rangef))]
    public class RangefDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var columnWidth = position.width / 2f;
            var height = position.height;

            var labelPos = new Rect(position.x, position.y, columnWidth, height);
            EditorGUI.LabelField(labelPos, label.text);

            DrawField(
                new Rect(position.x + columnWidth, position.y, 50, position.height),
                property.FindPropertyRelative("start")
            );

            DrawField(
                new Rect(position.x + columnWidth + 110, position.y, 50, position.height),
                property.FindPropertyRelative("end")
            );
        }

        private void DrawField(
            Rect rect,
            SerializedProperty value
        )
        {
            EditorGUI.LabelField(
                new Rect(rect.x, rect.y, rect.width, rect.height), 
                value.displayName
            );

            EditorGUI.PropertyField(
                new Rect(rect.x + rect.width, rect.y, rect.width, rect.height),
                value,
                GUIContent.none
            );
        }
    }
}