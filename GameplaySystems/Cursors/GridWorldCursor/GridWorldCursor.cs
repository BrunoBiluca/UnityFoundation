using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityFoundation.Code;
using UnityFoundation.Code.Grid;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.WorldCursors
{
    public class GridWorldCursor<T> : BilucaMono, IWorldCursor
    {
        [SerializeField] private LayerMask floorLayer;

        private IWorldGridXZ<T> worldGrid;
        private IRaycastHandler raycastHandler;

        public Optional<Vector3> WorldPosition { get; private set; }

        public Optional<Vector2> ScreenPosition { get; private set; }

        public event Action OnClick;
        public event Action OnSecondaryClick;

        protected override void OnAwake()
        {
            ScreenPosition = Optional<Vector2>.None();
            WorldPosition = Optional<Vector3>.None();
        }

        public virtual void Enable()
        {
            gameObject.SetActive(true);
        }

        public virtual void Disable()
        {
            gameObject.SetActive(false);
        }

        public void Setup(
            IRaycastHandler raycastHandler,
            IWorldGridXZ<T> worldGrid
        )
        {
            this.raycastHandler = raycastHandler;
            this.worldGrid = worldGrid;
        }

        public void Update()
        {
            if(raycastHandler == null) return;
            if(worldGrid == null) return;

            UpdateCursor();
        }

        private void UpdateCursor()
        {
            ScreenPosition = Optional<Vector2>.Some(Mouse.current.position.ReadValue());

            if(IgnoreClick())
            {
                ResetWorldPosition();
                return;
            }

            EvaluateWorldPosition();
            EvaluateButtonPressed();
        }

        private void EvaluateButtonPressed()
        {
            // TODO: configurar essas a��es de acordo com a configura��o do Input Actions
            if(Mouse.current.leftButton.wasPressedThisFrame)
                OnClick?.Invoke();

            if(Mouse.current.rightButton.wasPressedThisFrame)
                OnSecondaryClick?.Invoke();
        }

        private void EvaluateWorldPosition()
        {
            var worldPosition = raycastHandler
                .GetWorldPosition(ScreenPosition.Get(), floorLayer);

            if(!worldPosition.IsPresentAndGet(out Vector3 pos))
            {
                ResetWorldPosition();
                return;
            }

            try
            {
                WorldPosition = Optional<Vector3>.Some(worldGrid.GetCellCenterPosition(pos));
            }
            catch(ArgumentOutOfRangeException)
            {
                ResetWorldPosition();
            }
        }

        private void ResetWorldPosition()
        {
            WorldPosition = Optional<Vector3>.None();
        }

        private bool IgnoreClick()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }
    }
}