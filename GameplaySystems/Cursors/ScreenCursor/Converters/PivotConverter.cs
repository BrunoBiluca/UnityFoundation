using UnityEngine;
using UnityFoundation.Code;
using UnityFoundation.Code.DebugHelper;

namespace UnityFoundation.Cursors
{
    public class PivotConverter : IScreenPositionConverter
    {
        private Vector2 pivot;

        public Vector2 Direction { get; set; } = Vector2.one;

        public PivotConverter(Vector2 pivot)
        {
            UnityDebug.I.LogHighlight("PivotConverter", $"converting using pivot position {pivot}");
            this.pivot = pivot;
        }

        public Optional<Vector2> Eval(Vector2 screenPos)
        {
            var invertedPos = new Vector2(
                Direction.x * (screenPos.x - pivot.x),
                Direction.y * (screenPos.y - pivot.y)
            );
            return Optional<Vector2>.Some(invertedPos);
        }
    }
}