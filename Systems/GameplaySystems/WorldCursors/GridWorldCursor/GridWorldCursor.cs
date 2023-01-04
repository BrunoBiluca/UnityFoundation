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

        public void Setup(
            IRaycastHandler raycastHandler,
            IWorldGridXZ<T> worldGrid
        )
        {
            ScreenPosition = Optional<Vector2>.None();
            WorldPosition = Optional<Vector3>.None();
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

            if(IgnoreClick()) return;

            EvaluateWorldPosition();

            // TODO: configurar essas ações de acordo com a configuração do Input Actions
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
                WorldPosition = Optional<Vector3>.None();
                return;
            }

            try
            {
                WorldPosition = Optional<Vector3>.Some(worldGrid.GetCellCenterPosition(pos));
            }
            catch(ArgumentOutOfRangeException)
            {
                WorldPosition = Optional<Vector3>.None();
            }
        }

        private bool IgnoreClick()
        {
            Optional<Vector3>.None();
            return EventSystem.current.IsPointerOverGameObject();
        }
    }
}
