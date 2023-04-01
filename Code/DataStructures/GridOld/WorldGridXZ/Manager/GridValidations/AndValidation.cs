namespace UnityFoundation.Code.Grid
{
    public class AndValidation<T> : IGridValidation<T>
    {
        private readonly IGridValidation<T>[] validations;

        public AndValidation(params IGridValidation<T>[] validations)
        {
            this.validations = validations;
        }

        public bool IsAvailable(GridCellXZ<T> cell)
        {
            foreach(var validation in validations)
                if(!validation.IsAvailable(cell))
                    return false;

            return true;
        }
    }
}
