using System;

namespace UnityFoundation.Code
{
    public interface IGrid<TPosition, TValue>
    {
        void Clear(TPosition coordinate);
        TValue GetValue(TPosition coordinate);
        bool IsInsideGrid(TPosition coordinate);
        void SetValue(TPosition coordinate, TValue value);
        void UpdateValue(TPosition coordinate, Action<TValue> valueCallback);
    }
}
