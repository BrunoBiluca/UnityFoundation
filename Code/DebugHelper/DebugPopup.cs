using TMPro;
using UnityEngine;

namespace UnityFoundation.Code.DebugHelper
{
    public class SimplePopup : MonoBehaviour
    {
        private void Update()
        {
            transform.position += transform.up * Time.deltaTime;
        }
    }

    public static class DebugPopup
    {
        public static void Create(string text)
        {
            var gameObject = new GameObject(
                "debug_popup", typeof(TextMeshPro), typeof(SimplePopup)
            );

            var transform = gameObject.GetComponent<RectTransform>();
            var mousePosition = CameraUtils.GetMousePosition3D();
            transform.rotation = Camera.main.transform.rotation;
            transform.localPosition = mousePosition + new Vector3(0f, 0.1f, 0f);

            var textMesh = gameObject.GetComponent<TextMeshPro>();
            textMesh.text = text;
            textMesh.color = Color.white;
            textMesh.fontSize = 2;
            textMesh.alignment = TextAlignmentOptions.Midline;

            Object.Destroy(gameObject, 5f);
        }
    }
}
