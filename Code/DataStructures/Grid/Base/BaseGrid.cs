using System;
using System.Collections.Generic;

namespace UnityFoundation.Code
{
    public class BaseGrid<TGridLimits, TGridCell, TPosition, TValue> : IGrid<TPosition, TValue>
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
