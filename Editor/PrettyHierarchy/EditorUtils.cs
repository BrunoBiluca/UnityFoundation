using UnityEditor;

namespace UnityFoundation.Editor.Hierarchy
{
    public static class EditorUtils
    {
        public static bool IsHierarchyFocused {
            get {
                return EditorWindow.focusedWindow != null
                    && EditorWindow.focusedWindow.titleContent.text == "Hierarchy";
            }
        }
    }
}