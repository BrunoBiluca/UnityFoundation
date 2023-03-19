using System.Collections.Generic;

namespace UnityFoundation.Code
{
    public class GridXZLimits : IGridLimits<XZ>
    {
        private readonly int width;
        private readonly int depth;

        public GridXZLimits(int width, int depth)
        {
            this.width = width;
            this.depth = depth;
        }

        public int GetIndex(XZ coordinate)
        {
            return coordinate.X * depth + coordinate.Z;
        }

        public IEnumerable<int> GetIndexes()
        {
            for(int x = 0; x < width; x++)
                for(int z = 0; z < depth; z++)
                    yield return GetIndex(new XZ(x, z));
        }

        public bool IsInside(XZ coordiante)
        {
            return coordiante.X < width && coordiante.Z < depth;
        }
    }
}
