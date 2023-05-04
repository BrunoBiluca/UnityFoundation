using System;
using UnityEditor;
using UnityEngine;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.Editor
{
    [Obsolete]
    public class UnityControllerWindow : EditorWindow
    {
        private const string windowName = "Unity Controller";
        private const string path = UnityFoundationEditorConfig.BASE_WINDOW_PATH + windowName;
        private const string EDITOR_PREFS_AUTO_REFRESH = "kAutoRefresh";

        [MenuItem(path)]
        public static void ShowEditorWindow()
        {
            GetWindow<UnityControllerWindow>(windowName);
        }

        public void OnGUI()
        {
            var autoRefresh = EditorPrefs.GetBool(EDITOR_PREFS_AUTO_REFRESH);
            autoRefresh = GUILayout.Toggle(autoRefresh, "Auto refresh");
            EditorPrefs.SetBool(EDITOR_PREFS_AUTO_REFRESH, autoRefresh);

            if(GUILayout.Button("Refresh"))
            {
                AssetDatabase.Refresh();
            }
        }
    }
}