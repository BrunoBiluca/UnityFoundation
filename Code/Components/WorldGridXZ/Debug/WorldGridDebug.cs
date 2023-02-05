using UnityEngine;
using UnityFoundation.Code;
using UnityFoundation.Code.DebugHelper;
using UnityFoundation.Code.Grid;

namespace UnityFoundation.Code.Grid
{
    public class WorldGridDebug<T> : MonoBehaviour
    {
        [field: SerializeField] public bool DebugMode { get; private set; }

        private IWorldGridXZ<T> originalGrid;
        private IWorldGridXZ<GridDebugValue> debugGrid;

        public void Setup(IWorldGridXZ<T> grid)
        {
            originalGrid = grid;
            debugGrid = new WorldGridXZ<GridDebugValue>(
                grid.InitialPosition,
                grid.Width,
                grid.Depth,
                grid.CellSize
            );
            Display();
        }

        public void Update()
        {
            gameObject.SetActive(DebugMode);

            UpdateCells();
        }

        private void UpdateCells()
        {
            if(debugGrid == null) return;

            foreach(var c in debugGrid.Cells)
            {
                c.Value.SetText(originalGrid.Cells[c.Position.X, c.Position.Z].ToString());
            }
        }

        public void Display()
        {
            TransformUtils.RemoveChildObjects(transform);

            for(int x = 0; x < debugGrid.Cells.GetLength(0); x++)
                for(int z = 0; z < debugGrid.Cells.GetLength(1); z++)
                    DrawGridCell(x, z);

            GridDebug.DrawLines(debugGrid.Config, Time.deltaTime);
        }

        private void DrawGridCell(int x, int z)
        {
            var gridCellWorldPos = new Vector3(x * debugGrid.CellSize, 0f, z * debugGrid.CellSize);
            var cellWorldPos = debugGrid.GetCellWorldPosition(gridCellWorldPos);

            var text = DebugDraw.DrawWordTextCell(
                debugGrid.Cells[x, z].ToString(),
                cellWorldPos,
                new Vector3(debugGrid.CellSize, 0.5f, debugGrid.CellSize),
                fontSize: 2f,
                transform
            );

            debugGrid.TrySetValue(cellWorldPos, new GridDebugValue(text));
        }
    }
}
