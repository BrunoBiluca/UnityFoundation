using System;

namespace UnityFoundation.Code.Grid
{
    [Serializable]
    public class GridPositionXZ<TValue>
    {
        public IntXZ GridPosition { get; private set; }

        public int X => GridPosition.X;
        public int Z => GridPosition.Z;
        public int Index { get; set; }
        public TValue Value { get; set; }

        public GridPositionXZ(int x, int z)
        {
            GridPosition = new IntXZ(x, z);
            Value = default;
        }

        public GridPositionXZ(int x, int z, TValue value)
        {
            GridPosition = new IntXZ(x, z);
            Value = value;
        }

        public void SetIndex(int index)
        {
            Index = index;
        }

        public override string ToString()
        {
            return $"(x: {X}, z: {Z}) => {Value}";
        }
    }
}
