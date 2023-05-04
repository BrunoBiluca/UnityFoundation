using UnityEditor;
using UnityFoundation.Code.Grid;

namespace UnityFoundation.Editor
{
    [CustomEditor(typeof(WorldGridXZMono<>), true)]
    public class GridEditor : UnityEditor.Editor
    {
        double refreshSceneTime = 1f;
        double nextRefresh = 0f;

        public void OnSceneGUI()
        {
            if(EditorApplication.timeSinceStartup > nextRefresh)
            {
                nextRefresh = EditorApplication.timeSinceStartup + refreshSceneTime;
                var grid = target as IGridXZBase;
                GridDebug.DrawLines(grid.Config, (float)refreshSceneTime);
            }
        }
    }
}