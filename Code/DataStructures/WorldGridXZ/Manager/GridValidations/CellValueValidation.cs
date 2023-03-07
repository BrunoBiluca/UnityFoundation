using System;

namespace UnityFoundation.Code.Grid
{
    public class CellValueValidation<T> : IGridValidation<T>
    {
        private readonly Func<T, bool> callback;

        public CellValueValidation(Func<T, bool> callback)
        {
            this.callback = callback;
        }

        public bool IsAvailable(GridCellXZ<T> cell)
        {
            return callback(cell.Value);
        }
    }
}
