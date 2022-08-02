using UnityEngine;

namespace UnityFoundation.Code.Grid
{
    public class WorldGridXZ<T> : GridXZ<T>, IWorldGridXZ<T>
    {
        public WorldGridXZ(int width, int height, int cellSize)
            : base(width, height, cellSize)
        {
        }

        public Vector3 GetCellWorldPosition(Vector3 worldPosition)
        {
            var cellPos = GetCellPosition((int)worldPosition.x, (int)worldPosition.z);
            return new Vector3(cellPos.X, 0, cellPos.Z) * CellSize;
        }

        //        public virtual bool TrySetGridValue(Vector3 position, TValue value)
        //{
        //    var gridPosition = GetGridPosition((int)position.x, (int)position.z);

        //    if(!IsInsideGrid(gridPosition.X, gridPosition.Z))
        //        return false;

        //    gridArray[gridPosition.X, gridPosition.Z].Value = value;
        //    return true;
        //}

        //public virtual bool ClearGridValue(Vector3 position)
        //{
        //    var gridPosition = GetGridPosition((int)position.x, (int)position.z);

        //    if(!IsInsideGrid(gridPosition.X, gridPosition.Z))
        //        return false;

        //    gridArray[gridPosition.X, gridPosition.Z].Value = default;
        //    return true;
        //}

    }
}
