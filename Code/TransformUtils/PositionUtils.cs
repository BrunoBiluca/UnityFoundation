using UnityEngine;

namespace UnityFoundation.Code {
    public static class PositionUtils {
        public static Vector3 GetRandomPosition(float range) {
            return new Vector3(
                Random.Range(-range, range),
                Random.Range(-range, range)
            );
        }

        public static Vector3 GetRandomCirclePosition(Vector3 reference, float range) {
            float angle = Random.Range(0f, 2f) * Mathf.PI;
            float randomX = Random.Range(0, range);
            float randomY = Random.Range(0, range);
            return new Vector3(
                Mathf.Cos(angle) * randomX + reference.x,
                Mathf.Sin(angle) * randomY + reference.y
            );
        }

        public static Vector3 GetRandomSemiCirclePosition(
            Vector3 reference, float minRange, float maxRange
        ) {
            float angle = Random.Range(0f, 2f) * Mathf.PI;
            float randomX = Random.Range(minRange, maxRange);
            float randomY = Random.Range(minRange, maxRange);
            return new Vector3(
                Mathf.Cos(angle) * randomX + reference.x,
                Mathf.Sin(angle) * randomY + reference.y
            );
        }

    }
}