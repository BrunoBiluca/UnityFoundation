using log4net.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityFoundation.Code.DebugHelper;
using UnityFoundation.Code.Grid;
using UnityFoundation.Tools.Spline;

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
                GridDebug.DrawLines(grid, (float)refreshSceneTime);
            }
        }
    }
}