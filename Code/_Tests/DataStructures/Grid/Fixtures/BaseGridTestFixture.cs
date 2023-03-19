namespace UnityFoundation.Code.Tests
{
    public class BaseGridTestFixture : IGridTestFixture<XZ, int>
    {
        public virtual IGrid<XZ, int> Grid()
        {
            return new BaseGrid<GridXZLimits, GridCell<int>, XZ, int>(new GridXZLimits(2, 2));
        }

        public XZ Coordinate() => new(0, 0);

        public XZ SecondCoordinate() => new(1, 0);

        public XZ OutOfGridCoordinate() => new(3, 3);
    }
}
