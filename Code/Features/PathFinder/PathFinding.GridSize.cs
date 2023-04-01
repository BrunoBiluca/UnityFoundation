namespace UnityFoundation.Code.Algorithms
{
    public partial class PathFinding
    {
        public struct GridSize
        {
            public int Width { get; private set; }
            public int Height { get; private set; }

            public GridSize(int width, int height) : this()
            {
                Width = width;
                Height = height;
            }
        }
    }
}