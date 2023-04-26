using UnityEngine;

namespace UnityFoundation.Cursors
{
    public class InvertYConverter : PivotConverter
    {
        public InvertYConverter(Vector2 pivot) : base(pivot)
        {
            Direction = new(1, -1);
        }
    }
}