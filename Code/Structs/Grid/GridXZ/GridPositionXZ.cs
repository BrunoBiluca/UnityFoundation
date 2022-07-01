using System;

namespace UnityFoundation.Code.Grid
{
    [Serializable]
    public class GridPositionXZ<TValue>
    {
        private readonly IntXZ xz;

        public int X => xz.X;
        public int Z => xz.Z;
        public int Index { get; set; }
        public TValue Value { get; set; }

        public GridPositionXZ(int x, int z)
        {
            xz = new IntXZ(x, z);
            Value = default;
        }

        public GridPositionXZ(int x, int z, TValue value)
        {
            xz = new IntXZ(x, z);
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
