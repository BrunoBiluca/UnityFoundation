namespace UnityFoundation.Code
{
    public class XY
    {
        public int X { get; }
        public int Y { get; }

        public XY(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
