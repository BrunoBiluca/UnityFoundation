using System;

namespace UnityFoundation.Code.Grid
{
    [Serializable]
    public class GridCellXZ<TValue>
    {
        // TODO: essa classe pode expor mais informações
        // que são interessantes para quem a utiliza
        // - GridPositionXZ
        // - ScaledPositionXZ
        // O WorldGridCell pode também expor o WorldPosition da célula
        public int X { get; private set; }
        public int Z { get; private set; }
        public int Index { get; set; }
        public TValue Value { get; set; }

        public GridCellXZ(int x, int z)
        {
            Init(x, z, default);
        }

        public GridCellXZ(int x, int z, TValue value)
        {
            Init(x, z, value);
        }

        private void Init(int x, int z, TValue value)
        {
            X = x;
            Z = z;
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
