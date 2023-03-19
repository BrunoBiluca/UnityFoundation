namespace UnityFoundation.Code.Tests
{
    public interface IGridTestFixture<TPosition, TValue>
    {
        TPosition Coordinate();
        public abstract IGrid<TPosition, TValue> Grid();
        TPosition OutOfGridCoordinate();
        TPosition SecondCoordinate();
    }
}
