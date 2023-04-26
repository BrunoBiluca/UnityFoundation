using UnityEngine;
using UnityFoundation.Code;
using UnityFoundation.Code.DebugHelper;

namespace UnityFoundation.Cursors
{
    public class BoundariesChecker : IScreenPositionConverter
    {
        private Rect rect;

        public Vector2 Direction { get; set; } = Vector2.one;

        public BoundariesChecker(Rect rect)
        {
            UnityDebug.I.LogHighlight("BoundariesChecker", $"converting using rect {rect}");
            this.rect = rect;
        }

        public Optional<Vector2> Eval(Vector2 screenPos)
        {
            var evalRect = new Rect(rect.position, rect.size * Direction);
            if(evalRect.Contains(screenPos, allowInverse: true))
            {
                return Optional<Vector2>.Some(screenPos);
            }

            return Optional<Vector2>.None();
        }
    }
}