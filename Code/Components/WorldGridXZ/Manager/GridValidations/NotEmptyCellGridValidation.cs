namespace UnityFoundation.Code.Grid
{
    public class NotEmptyCellGridValidation<T> : IGridValidation<T>
    {
        public bool IsAvailable(GridCellXZ<T> cell)
        {
            return !cell.IsEmpty();
        }
    }
}
