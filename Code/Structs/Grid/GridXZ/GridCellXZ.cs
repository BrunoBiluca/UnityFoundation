using System;

namespace UnityFoundation.Code.Grid
{
    [Serializable]
    public class GridCellXZ<TValue> : IEmptyable
    {
        // Ter uma interface para GridPosition e ScaledPosition
        public int X { get; private set; }
        public int Z { get; private set; }
        public TValue Value { get; set; }

        public GridCellXZ(int x, int z) : this(x, z, default)
        {
        }

        public GridCellXZ(int x, int z, TValue value)
        {
            X = x;
            Z = z;
            Value = value;
        }

        public bool IsEmpty()
        {
            if(Value is IEmptyable val)
                return val.IsEmpty();

            return Equals(Value, default(TValue));
        }

        public override string ToString()
        {
            return $"(x: {X}, z: {Z}) => {Value}";
        }
    }
}
