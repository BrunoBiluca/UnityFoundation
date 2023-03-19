namespace UnityFoundation.Code
{
    public class GridCell<TValue> : IGridCell<TValue> where TValue : new()
    {
        private TValue value;

        public bool IsEmpty { get; private set; } = true;

        public TValue GetValue() => value;

        public GridCell()
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
}
