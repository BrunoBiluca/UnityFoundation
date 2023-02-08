using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityFoundation.Code;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.WorldCursors
{
    public class FreeWorldCursor : Singleton<FreeWorldCursor>, IWorldCursor
    {
        [SerializeField] private LayerMask floorMask;

        private RaycastHandler raycastHandler;

        public event Action OnClick;
        public event Action OnSecondaryClick;

        public Optional<Vector3> WorldPosition { get; private set; }
        public Optional<Vector2> ScreenPosition { get; private set; }

        protected override void OnAwake()
        {
            raycastHandler = new RaycastHandler(new CameraDecorator(Camera.main));

            WorldPosition = Optional<Vector3>.None();
            ScreenPosition = Optional<Vector2>.None();
        }

        public void Update()
        {
            ScreenPosition = Optional<Vector2>.Some(Mouse.current.position.ReadValue());

            WorldPosition = raycastHandler
                .GetWorldPosition(ScreenPosition.Get(), floorMask);

            if(Mouse.current.leftButton.wasPressedThisFrame)
                OnClick?.Invoke();

            if(Mouse.current.rightButton.wasPressedThisFrame)
                OnSecondaryClick?.Invoke();
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}