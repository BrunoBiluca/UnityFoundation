using UnityEditor;
using UnityEngine;
using UnityFoundation.Code.UnityAdapter;

namespace Assets.UnityFoundation.Editor
{
    [CustomPropertyDrawer(typeof(ShowOnlyAttribute))]
    public class ShowOnlyAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(
            Rect position, SerializedProperty prop, GUIContent label
        )
        {
            string valueStr;

            switch(prop.propertyType)
            {
                case SerializedPropertyType.Integer:
                    valueStr = prop.intValue.ToString();
                    break;
                case SerializedPropertyType.Boolean:
                    valueStr = prop.boolValue.ToString();
                    break;
                case SerializedPropertyType.Float:
                    valueStr = prop.floatValue.ToString("0.00000");
                    break;
                case SerializedPropertyType.String:
                    valueStr = prop.stringValue;
                    break;
                case SerializedPropertyType.Vector2:
                    valueStr = $"X: {prop.vector2Value.x} - Y: {prop.vector2Value.y}";
                    break;
                default:
                    valueStr = "(not supported)";
                    break;
            }

            EditorGUI.LabelField(position, label.text, valueStr);
        }
    }
}
