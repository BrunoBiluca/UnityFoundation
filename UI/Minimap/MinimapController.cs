using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Assets.UnityFoundation.UI.Minimap
{
    public class MinimapController : MonoBehaviour, IPointerDownHandler, IDragHandler
    {
        public const string MINIMAP_CAMERA_TAG = "minimap_camera";

        [SerializeField] private float offset = -6f;

        public Transform TargetTransform { get; set; }

        private RectTransform minimapRect;
        private float mapScale;

        private void Awake()
        {
            minimapRect = GetComponent<RectTransform>();
        }

        private void Start()
        {
            SetMapScale();
        }

        private void SetMapScale()
        {
            var minimapCamera = GameObject.FindGameObjectWithTag(MINIMAP_CAMERA_TAG);

            if(
                minimapCamera == null 
                || !minimapCamera.TryGetComponent(out Camera camera)
            )
                throw new MissingComponentException(
                    "Minimap camera is not set in the current scene"
                );

            // mapScale is half of scene or floor size, if floor pivot is cetered
            mapScale = camera.orthographicSize;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            MoveCamera();
        }

        public void OnDrag(PointerEventData eventData)
        {
            MoveCamera();
        }

        private void MoveCamera()
        {
            var mousePos = Mouse.current.position.ReadValue();

            var isWithinMinimap = RectTransformUtility.ScreenPointToLocalPointInRectangle(
                minimapRect,
                mousePos,
                null,
                out Vector2 localPoint
            );
            if(!isWithinMinimap) return;

            Vector2 lerp = new Vector2(
                (localPoint.x - minimapRect.rect.x) / minimapRect.rect.width,
                (localPoint.y - minimapRect.rect.y) / minimapRect.rect.height
            );

            Vector3 newCameraPos = new Vector3(
                Mathf.Lerp(-mapScale, mapScale, lerp.x),
                TargetTransform.position.y,
                Mathf.Lerp(-mapScale, mapScale, lerp.y)
            );

            TargetTransform.position = newCameraPos + new Vector3(0f, 0f, offset);
        }
    }
}