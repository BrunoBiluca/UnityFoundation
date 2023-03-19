namespace UnityFoundation.Code.Tests
{
    public class GridXZTestFixture : BaseGridTestFixture
    {
        public override IGrid<XZ, int> Grid()
        {
            return new GridXZ<int>(new GridXZLimits(2, 2));
        }
    }
}
