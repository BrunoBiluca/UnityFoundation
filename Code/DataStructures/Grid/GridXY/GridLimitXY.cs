using System;
using System.Collections.Generic;

namespace UnityFoundation.Code
{
    [Serializable]
    public class GridLimitXY : IGridLimits<XY>
    {
        public int Width { get; }
        public int Height { get; }
        public int Positions => Width * Height;

        public GridLimitXY(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int GetIndex(XY coordinate)
        {
            return coordinate.X * Width + coordinate.Y;
        }

        public XY GetCoordinate(int index)
        {
            return new(index / Width, index % Width);
        }

        public IEnumerable<int> GetIndexes()
        {
            foreach(var coord in GetAllCoordinates())
                yield return GetIndex(coord);
        }

        public IEnumerable<XY> GetAllCoordinates()
        {
            for(int x = 0; x < Width; x++)
                for(int y = 0; y < Height; y++)
                    yield return new(x, y);
        }

        public bool IsInside(XY coordinate)
        {
            return coordinate.X >= 0
                && coordinate.X < Width
                && coordinate.Y >= 0
                && coordinate.Y < Height;
        }
    }
}
