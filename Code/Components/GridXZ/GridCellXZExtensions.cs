using UnityEngine;

namespace UnityFoundation.Code.Grid
{
    public static class GridCellXZExtensions
    {
        public static bool IsInRange<T>(this GridCellXZ<T> a, GridCellXZ<T> b, int range)
        {
            var x = Mathf.Abs(a.Position.X - b.Position.X);
            var z = Mathf.Abs(a.Position.Z - b.Position.Z);

            return x + z <= range;
        }

        public static int Range<T>(this GridCellXZ<T> a, GridCellXZ<T> b)
        {
            var x = Mathf.Abs(a.Position.X - b.Position.X);
            var z = Mathf.Abs(a.Position.Z - b.Position.Z);

            return x + z;
        }
    }
}
