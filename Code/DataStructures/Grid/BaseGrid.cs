using System;
using System.Collections.Generic;

namespace UnityFoundation.Code
{
    public interface IGridLimits<TPosition>
    {
        bool IsInside(TPosition coordinate);
        int GetIndex(TPosition coordinate);
        IEnumerable<int> GetIndexes();
    }

    public class GridXZLimits : IGridLimits<XZ>
    {
        private readonly int width;
        private readonly int depth;

        public GridXZLimits(int width, int depth)
        {
            this.width = width;
            this.depth = depth;
        }

        public int GetIndex(XZ coordinate)
        {
            return coordinate.X * depth + coordinate.Z;
        }

        public IEnumerable<int> GetIndexes()
        {
            for(int x = 0; x < width; x++)
                for(int z = 0; z < depth; z++)
                    yield return GetIndex(new XZ(x, z));
        }

        public bool IsInside(XZ coordiante)
        {
            return coordiante.X < width && coordiante.Z < depth;
        }
    }

    public class XZ
    {
        public int X { get; }
        public int Z { get; }

        public XZ(int x, int z)
        {
            X = x;
            Z = z;
        }
    }

    public interface IGridCell<TValue> where TValue : new()
    {
        bool IsEmpty { get; }
        void Clear();
        TValue GetValue();
        void SetValue(TValue value);
    }

    public class GridXZCell<TValue> : IGridCell<TValue> where TValue : new()
    {
        private TValue value;

        public bool IsEmpty { get; private set; } = true;

        public TValue GetValue() => value;

        public GridXZCell()
        {
            value = new();
        }

        public void SetValue(TValue value)
        {
            if(IsEmpty)
            {
                this.value = value;
                IsEmpty = false;
            }
        }

        public void Clear()
        {
            IsEmpty = true;
            value = default;
        }
    }

    public class BaseGrid<TGridLimits, TGridCell, TPosition, TValue>
        where TGridLimits : IGridLimits<TPosition>
        where TGridCell : IGridCell<TValue>, new()
        where TValue : new()
    {
        private readonly TGridLimits limits;

        private readonly Dictionary<int, TGridCell> cells;

        public BaseGrid(TGridLimits limits)
        {
            this.limits = limits;

            cells = new();
            foreach(var index in limits.GetIndexes())
                cells.Add(index, new());
        }

        public bool IsInsideGrid(TPosition coordinate)
        {
            return limits.IsInside(coordinate);
        }

        public TValue GetValue(TPosition coordinate)
        {
            return GetCell(coordinate).GetValue();
        }

        public void SetValue(TPosition coordinate, TValue value)
        {
            GetCell(coordinate).SetValue(value);
        }

        private TGridCell GetCell(TPosition coordinate)
        {
            if(!IsInsideGrid(coordinate))
                throw new ArgumentOutOfRangeException();

            return cells[limits.GetIndex(coordinate)];
        }

        public void UpdateValue(TPosition coordinate, Action<TValue> valueCallback)
        {
            valueCallback(GetCell(coordinate).GetValue());
        }

        public void Clear(TPosition coordinate)
        {
            GetCell(coordinate).Clear();
        }
    }
}
