namespace UnityFoundation.Code.Grid
{
    public class RangeGridValidation<T> : IGridValidation<T>
    {
        public GridCellXZ<T> CurrCell { get; }
        public int Range { get; }

        public RangeGridValidation(GridCellXZ<T> currCell, int range)
        {
            CurrCell = currCell;
            Range = range;
        }

        public bool IsAvailable(GridCellXZ<T> cell)
        {
            return cell.IsInRange(CurrCell, Range);
        }
    }
}
