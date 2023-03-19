namespace UnityFoundation.Code
{
    public class GridXY<TValue> : BaseGrid<GridLimitXY, GridCell<TValue>, XY, TValue>
        where TValue : new()
    {
        public GridXY(GridLimitXY limits) : base(limits)
        {
        }
    }
}
