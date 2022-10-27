using TMPro;
using UnityEngine;

namespace UnityFoundation.Code.DebugHelper
{
    public static class DebugDraw
    {

        public static void DrawRectangle(Vector3 position, Vector3 size, Color color)
        {
            // Top line
            Debug.DrawLine(
                new Vector3(position.x - size.x / 2, position.y - size.y / 2, position.z),
                new Vector3(position.x + size.x / 2, position.y - size.y / 2, position.z),
                color
            );
            // BottomLine
            Debug.DrawLine(
                new Vector3(position.x - size.x / 2, position.y + size.y / 2, position.z),
                new Vector3(position.x + size.x / 2, position.y + size.y / 2, position.z),
                color
            );
            // Left Line
            Debug.DrawLine(
                new Vector3(position.x - size.x / 2, position.y - size.y / 2, position.z),
                new Vector3(position.x - size.x / 2, position.y + size.y / 2, position.z),
                color
            );
            // Right Line
            Debug.DrawLine(
                new Vector3(position.x + size.x / 2, position.y - size.y / 2, position.z),
                new Vector3(position.x + size.x / 2, position.y + size.y / 2, position.z),
                color
            );
        }

        public static void DrawCircle(
            Vector3 position,
            float radius,
            Color color,
            float height = 0f,
            bool isHorizontal = false
        )
        {
            const int numberOfSlices = 16;
            for(var i = 0; i < numberOfSlices; i++)
            {
                var currentPI = Mathf.PI * i / (numberOfSlices / 2);
                var nextPI = Mathf.PI * (i + 1) / (numberOfSlices / 2);
                var start = new Vector3(
                    position.x + radius * Mathf.Sin(currentPI),
                    position.y + radius * Mathf.Cos(currentPI),
                    position.z + height
                );

                var end = new Vector3(
                    position.x + radius * Mathf.Sin(nextPI),
                    position.y + radius * Mathf.Cos(nextPI),
                    position.z + height
                );

                if(isHorizontal)
                {
                    start = new Vector3(
                        position.x + radius * Mathf.Sin(currentPI),
                        position.y + height,
                        position.z + radius * Mathf.Cos(currentPI)
                    );

                    end = new Vector3(
                        position.x + radius * Mathf.Sin(nextPI),
                        position.y + height,
                        position.z + radius * Mathf.Cos(nextPI)
                    );
                }

                Debug.DrawLine(start, end, color);
            }
        }

        public static void DrawSphere(Vector3 position, float radius, Color color)
        {
            var interval = radius / 10f;
            for(var i = 0f; i <= radius; i += interval)
            {
                var lradius = radius - i;

                DrawCircle(position, lradius, color, height: i);
                DrawCircle(position, lradius, color, height: -i);
                DrawCircle(position, lradius, color, height: i, isHorizontal: true);
                DrawCircle(position, lradius, color, height: -i, isHorizontal: true);
            }
        }

        public static TextMeshPro DrawWordTextCell(
            string text,
            Vector3 localPosition,
            Vector3 cellSize,
            float fontSize = 2f,
            Transform parent = null
        )
        {
            var gameObject = new GameObject("world_text_cell", typeof(TextMeshPro));

            var transform = gameObject.GetComponent<RectTransform>();
            transform.sizeDelta = cellSize;
            transform.SetParent(parent, false);
            transform.localPosition = localPosition
                + new Vector3(cellSize.x / 2, cellSize.y / 2, cellSize.z / 2);
            transform.localRotation = Quaternion.Euler(90f, 0, 0);

            var textMesh = gameObject.GetComponent<TextMeshPro>();
            textMesh.text = text;
            textMesh.color = Color.white;
            textMesh.fontSize = fontSize;
            textMesh.alignment = TextAlignmentOptions.Midline;

            return textMesh;
        }

    }
}