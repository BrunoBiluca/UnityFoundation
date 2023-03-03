using System.Reflection;
using UnityEditor;

namespace UnityFoundation.Code
{
    public static class SerializedPropertyExtensions
    {
        public static T GetField<T>(this SerializedProperty property)
        {
            var targetObject = property.serializedObject.targetObject;
            var targetObjectClassType = targetObject.GetType();
            var field = targetObjectClassType.GetField(
                property.propertyPath,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
            );
            if(field != null)
                return (T)field.GetValue(targetObject);

            var baseField = targetObjectClassType.BaseType.GetField(
                property.propertyPath,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
            );

            if(baseField != null)
                return (T)baseField.GetValue(targetObject);

            return default;
        }
    }
}
