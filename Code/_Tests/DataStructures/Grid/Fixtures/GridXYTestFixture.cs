namespace UnityFoundation.Code.Tests
{
    public class GridXYTestFixture : IGridTestFixture<XY, int>
    {
        public XY Coordinate() => new(0, 0);

        public IGrid<XY, int> Grid()
        {
            return new GridXY<int>(new GridLimitXY(2, 2));
        }

        public XY OutOfGridCoordinate() => new(3, 3);

        public XY SecondCoordinate() => new(1, 0);
    }
}
