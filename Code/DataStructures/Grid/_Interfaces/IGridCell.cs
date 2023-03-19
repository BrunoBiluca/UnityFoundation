namespace UnityFoundation.Code
{
    public interface IGridCell<TValue> where TValue : new()
    {
        bool IsEmpty { get; }
        void Clear();
        TValue GetValue();
        void SetValue(TValue value);
    }
}
