using UnityEditor;
using UnityEngine;
using UnityFoundation.Tools.Spline;

namespace UnityFoundation.Tools.SplineEditor
{
    [CustomEditor(typeof(SplineMono))]
    public class SplineEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var currentSpline = (SplineMono)target;

            if(GUILayout.Button("Add Anchor"))
            {
                Undo.RecordObject(currentSpline, "Add Anchor");
                currentSpline.AddAnchor();
            }

            if(GUILayout.Button("Set All Y = 0"))
            {
                Undo.RecordObject(currentSpline, "Add Anchor");
                foreach(var anchor in currentSpline.Anchors)
                {
                    anchor.Origin.Position = new Vector3(
                        anchor.Origin.Position.x,
                        0f,
                        anchor.Origin.Position.z
                    );
                    anchor.PointA.Position = new Vector3(
                        anchor.PointA.Position.x,
                        0f,
                        anchor.PointA.Position.z
                    );
                    anchor.PointB.Position = new Vector3(
                        anchor.PointB.Position.x,
                        0f,
                        anchor.PointB.Position.z
                    );
                }
            }

            currentSpline.ClosedLoop = GUILayout.Toggle(
                currentSpline.ClosedLoop, nameof(currentSpline.ClosedLoop)
            );

            EditorGUILayout.PropertyField(serializedObject.FindProperty("anchors"));

            serializedObject.ApplyModifiedProperties();
        }

        private void OnSceneGUI()
        {
            var currentSpline = (SplineMono)target;
            foreach(var anchor in currentSpline.Anchors)
            {
                DrawAnchor(currentSpline, anchor);
            }

            DrawBezierCurve(currentSpline);
        }

        private void DrawBezierCurve(SplineMono spline)
        {
            for(int i = 0; i < spline.Anchors.Count - 1; i++)
            {
                var anchor = spline.Anchors[i];
                var nextAnchor = spline.Anchors[i + 1];

                Handles.DrawBezier(
                    spline.GetPosition(anchor.Origin),
                    spline.GetPosition(nextAnchor.Origin),
                    spline.GetPosition(anchor.PointB),
                    spline.GetPosition(nextAnchor.PointA),
                    Color.grey,
                    null,
                    3f
                );
            }

            if(spline.ClosedLoop)
            {
                var anchor = spline.Anchors[spline.Anchors.Count - 1];
                var nextAnchor = spline.Anchors[0];

                Handles.DrawBezier(
                    spline.GetPosition(anchor.Origin),
                    spline.GetPosition(nextAnchor.Origin),
                    spline.GetPosition(anchor.PointB),
                    spline.GetPosition(nextAnchor.PointA),
                    Color.grey,
                    null,
                    3f
                );
            }
        }

        private void DrawAnchor(
            SplineMono spline,
            SplineAnchor anchor
        )
        {
            Handles.color = Color.white;
            Handles.DrawWireCube(
                spline.GetPosition(anchor.Origin),
                Vector3.one * .5f
            );

            SplinePointPositionHandler(spline, anchor.Origin);

            DrawAnchorSidePoints(spline, anchor.PointA, Color.green);
            DrawAnchorSidePoints(spline, anchor.PointB, Color.blue);

            Handles.color = Color.white;
            Handles.DrawLine(
                spline.GetPosition(anchor.Origin),
                spline.GetPosition(anchor.PointA)
            );
            Handles.DrawLine(
                spline.GetPosition(anchor.Origin),
                spline.GetPosition(anchor.PointB)
            );
        }

        private void DrawAnchorSidePoints(
            SplineMono spline, SplinePoint point, Color sphereColor
        )
        {
            Handles.color = sphereColor;
            Handles.SphereHandleCap(
                0,
                spline.GetPosition(point),
                Quaternion.identity, .5f,
                EventType.Repaint
            );
            SplinePointPositionHandler(spline, point);
        }

        private static void SplinePointPositionHandler(
            SplineMono currentSpline,
            SplinePoint point
        )
        {
            EditorGUI.BeginChangeCheck();
            var newPosition = Handles.PositionHandle(
                currentSpline.GetPosition(point),
                Quaternion.identity
            );

            if(EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(currentSpline, "Change Anchor Position");
                currentSpline.SetPointPosition(point, newPosition);
            }
        }
    }
}