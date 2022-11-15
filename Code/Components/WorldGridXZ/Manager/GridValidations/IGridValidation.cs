namespace UnityFoundation.Code.Grid
{
    public interface IGridValidation<T>
    {
        bool IsAvailable(GridCellXZ<T> cell);
    }
}
