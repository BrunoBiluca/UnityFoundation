using System;
using UnityEngine;
using UnityFoundation.Code;

namespace UnityFoundation.Cursors
{
    public class ScreenPosition
    {
        /// <summary>
        /// (0, 0) is the bottom-left point of the screen
        /// </summary>
        public Optional<Vector2> Original { get; private set; } = Optional<Vector2>.None();
        public Optional<Vector2> Value => Converter?.Eval(Original.Get()) ?? Original;
        public IScreenPositionConverter Converter { get; set; }

        public ScreenPosition(Optional<Vector2> position)
        {
            Original = position;
        }

        public void SetOriginal(Vector2 position)
        {
            Original = Optional<Vector2>.Some(position);
        }

        public void Reset()
        {
            Original = Optional<Vector2>.None();
        }

        public ScreenPosition Copy()
        {
            return new(Original) { Converter = Converter };
        }
    }

    public class ScreenCursor : IScreenCursor
    {
        public ScreenPosition ScreenPosition { get; private set; } = new(Optional<Vector2>.None());
        public bool IsEnabled { get; private set; } = true;
        public float MinDragDistance { get; set; }

        public event Action OnClick;
        public event Action<CursorDrag> OnDrag;
        public event Action OnReleased;

        private readonly ICursorInput input;
        private Optional<ScreenPosition> initialDragPosition = Optional<ScreenPosition>.None();

        public ScreenCursor(ICursorInput input)
        {
            this.input = input;
        }

        public void Disable()
        {
            IsEnabled = false;
            ScreenPosition.Reset();
        }

        public void Enable()
        {
            IsEnabled = true;
        }

        public void SetConverter(IScreenPositionConverter converter)
        {
            ScreenPosition.Converter = converter;
        }

        public void Update(float detalTime = 0)
        {
            if(!IsEnabled) return;

            ScreenPosition.SetOriginal(input.Position);

            if(input.WasPressed) HandlePress();
            if(input.WasReleased) HandleReleased();
        }

        private void HandlePress()
        {
            OnClick?.Invoke();
            initialDragPosition = Optional<ScreenPosition>.Some(ScreenPosition.Copy());
        }

        private void HandleReleased()
        {
            OnReleased?.Invoke();

            EvaluateDrag();
            initialDragPosition = Optional<ScreenPosition>.None();
        }

        private void EvaluateDrag()
        {
            if(!initialDragPosition.IsPresentAndGet(out ScreenPosition initialDragPos))
                return;

            if(!initialDragPos.Value.IsPresentAndGet(out Vector2 initial))
                return;

            if(!ScreenPosition.Value.IsPresentAndGet(out Vector2 endPosition))
                return;

            var direction = endPosition - initial;

            if(Mathf.Abs(direction.x) < MinDragDistance)
                direction.x = 0f;

            if(Mathf.Abs(direction.y) < MinDragDistance)
                direction.y = 0f;

            if(direction.magnitude == 0f)
                return;

            OnDrag?.Invoke(new(initialDragPos, ScreenPosition.Copy(), direction.normalized));
        }
    }
}