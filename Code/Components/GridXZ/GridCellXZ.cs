using System;

namespace UnityFoundation.Code.Grid
{
    [Serializable]
    public class GridCellXZ<TValue> : IEmptyable
    {
        public GridCellPositionXZ Position { get; private set; }
        public TValue Value { get; set; }

        public GridCellXZ(int x, int z) : this(x, z, default)
        {
        }

        public GridCellXZ(int x, int z, TValue value)
        {
            Position = new GridCellPositionXZ(x, z);
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
            return $"(x: {Position.X}, z: {Position.Z}) => {Value}";
        }

        public void Clear()
        {
            if(Value is IEmptyable val)
            {
                val.Clear();
                return;
            }

            Value = default;
        }
    }
}
