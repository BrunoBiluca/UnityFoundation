using UnityEditor;
using Assets.UnityFoundation.EditorInspector;

namespace Assets.UnityFoundation.Editor
{
    [InitializeOnLoad]
    public static class PlayStateNotifier
    {
        static PlayStateNotifier()
        {
            EditorApplication.playModeStateChanged += ModeChanged;
        }

        static void ModeChanged(PlayModeStateChange playModeState)
        {
            PlayState.IsPlayMode
                = playModeState == PlayModeStateChange.EnteredPlayMode;

            PlayState.IsEditMode 
                = playModeState == PlayModeStateChange.EnteredEditMode;
        }
    }
}