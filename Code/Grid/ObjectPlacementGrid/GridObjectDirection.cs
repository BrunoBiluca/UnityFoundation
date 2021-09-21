using Assets.UnityFoundation.Code.Common;
using Unity.Mathematics;

namespace Assets.UnityFoundation.Code.Grid.ObjectPlacementGrid
{
    public class GridObjectDirection : EnumX<GridObjectDirection>
    {
        public static readonly GridObjectDirection UP
            = new GridObjectDirection(0, "UP", 180f, new int2(1, 1));

        public static readonly GridObjectDirection RIGHT
            = new GridObjectDirection(1, "RIGHT", 270f, new int2(1, 0));

        public static readonly GridObjectDirection DOWN
            = new GridObjectDirection(2, "DOWN", 0f, new int2(0, 0));

        public static readonly GridObjectDirection LEFT
            = new GridObjectDirection(3, "LEFT", 90f, new int2(0, 1));

        public GridObjectDirection(int index, string name, float rotation, int2 offset)
            : base(index, name)
        {
            Offset = offset;
            Rotation = rotation;
        }

        public float Rotation { get; }

        public int2 Offset { get; }

        public override string ToString()
        {
            return $"{name} {Rotation}";
        }
    }
}