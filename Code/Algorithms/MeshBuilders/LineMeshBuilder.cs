using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.Code.MeshBuilders
{
    public class LineMeshBuilder
    {

        private readonly Mesh mesh;
        private readonly List<Vector3> points;

        private float width = 1f;
        private Vector3 normal;

        private Vector3[] vertices;
        private Vector2[] uv;
        private int[] triangles;

        public LineMeshBuilder()
        {
            mesh = new Mesh();
            normal = Vector3.up;
            points = new List<Vector3>();
        }

        public LineMeshBuilder Width(float width)
        {
            this.width = width;
            return this;
        }

        public LineMeshBuilder AddPoint(Vector3 point)
        {
            points.Add(point);
            return this;
        }

        public Mesh Build()
        {
            SetupMesh();
            return mesh;
        }

        private void SetupMesh()
        {
            // 2 vertices per point, one "left" one "right"
            vertices = new Vector3[points.Count * 2];
            uv = new Vector2[points.Count * 2];

            // 2 triangles per point
            triangles = new int[(points.Count - 1) * 6];

            var widthHalf = width * .5f;

            var totalLength = CalculateLength();
            var currentLength = 0f;

            for(int index = 1; index < points.Count; index++)
            {
                var pointA = points[index - 1];
                var pointB = points[index];

                currentLength += Vector3.Distance(pointA, pointB);

                CalculatePoint(widthHalf, currentLength / totalLength, index);
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uv;
        }

        private void CalculatePoint(
            float widthHalf,
            float pointLength,
            int index
        )
        {
            var pointA = points[index - 1];
            var pointB = points[index];

            var directionAToB = (pointB - pointA).normalized;

            var crossLeft = Vector3.Cross(directionAToB, normal * 1f);
            var crossRight = Vector3.Cross(directionAToB, normal * -1f);

            // Relocate vertices
            var vIndex0 = (index - 1) * 2;
            var vIndex1 = vIndex0 + 1;
            var vIndex2 = vIndex0 + 2;
            var vIndex3 = vIndex0 + 3;

            if(index == 1)
            {
                vertices[0] = pointA + crossLeft * widthHalf;
                vertices[1] = pointA + crossRight * widthHalf;

                uv[0] = new Vector2(0, 0);
                uv[1] = new Vector2(1, 0);
            }

            vertices[vIndex2] = pointB + crossLeft * widthHalf;
            vertices[vIndex3] = pointB + crossRight * widthHalf;

            uv[vIndex2] = new Vector2(0, pointLength);
            uv[vIndex3] = new Vector2(1, pointLength);

            // Create triangles
            var tIndex = (index - 1) * 6;
            triangles[tIndex] = vIndex0;
            triangles[tIndex + 1] = vIndex2;
            triangles[tIndex + 2] = vIndex1;

            triangles[tIndex + 3] = vIndex1;
            triangles[tIndex + 4] = vIndex2;
            triangles[tIndex + 5] = vIndex3;
        }

        private float CalculateLength()
        {
            float totalLength = 0f;
            Vector3 lastPoint = points[0];
            for(int i = 0; i <= points.Count - 2; i += 2)
            {
                totalLength += Vector3.Distance(lastPoint, points[i]);
                lastPoint = points[i];
            }
            return totalLength;
        }
    }
}