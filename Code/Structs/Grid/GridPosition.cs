namespace UnityFoundation.Code.Grid
{
    public class GridPosition<TValue>
    {
        public int X { get; }
        public int Y { get; }

        public TValue Value { get; set; }

        public GridPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"({X}, {Y}) => {Value}";
        }
    }
}
