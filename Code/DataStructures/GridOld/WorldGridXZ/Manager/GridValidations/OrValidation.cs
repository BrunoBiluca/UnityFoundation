namespace UnityFoundation.Code.Grid
{
    public class OrValidation<T> : IGridValidation<T>
    {
        private readonly IGridValidation<T>[] validations;

        public OrValidation(params IGridValidation<T>[] validations)
        {
            this.validations = validations;
        }

        public bool IsAvailable(GridCellXZ<T> cell)
        {
            foreach(var v in validations)
            {
                if(v.IsAvailable(cell))
                    return true;
            }
            return false;
        }
    }
}
