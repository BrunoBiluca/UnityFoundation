namespace UnityFoundation.Code.Grid
{

    public class EmptyCellGridValidation<T> : IGridValidation<T>
    {
        public bool IsAvailable(GridCellXZ<T> cell)
        {
            return cell.IsEmpty();
        }
    }
}
