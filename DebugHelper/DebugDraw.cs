using UnityEngine;

namespace Assets.UnityFoundation.DebugHelper {
    public class DebugDraw {

        public static void DrawRectangle(Vector3 position, Vector3 size, Color color) {
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

        public static void DrawCircle(Vector3 position, float radius, Color color) {
            const int numberOfSlices = 16;
            for(int i = 0; i < numberOfSlices; i++) {
                var currentPI = Mathf.PI * i / (numberOfSlices / 2);
                var nextPI = Mathf.PI * (i + 1) / (numberOfSlices / 2);
                Vector3 start = new Vector3(position.x + radius * Mathf.Sin(currentPI), position.y + radius * Mathf.Cos(currentPI));

                Debug.DrawLine(
                    start,
                    new Vector3(position.x + radius * Mathf.Sin(nextPI), position.y + radius * Mathf.Cos(nextPI)),
                    color
                );
            }
        }

    }
}