namespace UnityFoundation.Code.Grid
{
    public class GridPositionXZ<TValue>
    {
        private readonly IntXZ xz;

        public int X => xz.X;
        public int Z => xz.Z;

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

        public override string ToString()
        {
            return $"(x: {X}, z: {Z}) => {Value}";
        }
    }
}
