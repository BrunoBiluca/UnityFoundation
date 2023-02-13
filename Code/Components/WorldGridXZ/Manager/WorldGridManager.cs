using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityFoundation.Code.Grid
{
    public class WorldGridManager<T>
    {
        public IWorldGridXZ<T> Grid { get; private set; }

        public List<IGridValidation<T>> gridValidations;

        public WorldGridManager(
            IWorldGridXZ<T> worldGrid
        )
        {
            Grid = worldGrid;
            gridValidations = new List<IGridValidation<T>>();
        }

        public virtual IEnumerable<GridCellXZ<T>> GetAllAvailableCells()
        {
            foreach(var c in Grid.Cells)
                if(IsCellAvailable(c))
                    yield return c;
        }

        public virtual IEnumerable<GridCellXZ<T>> GetCells(IGridValidation<T>[] validations)
        {
            foreach(var c in Grid.Cells)
                if(validations.All(v => v.IsAvailable(c)))
                    yield return c;
        }

        public bool IsCellAvailable(GridCellXZ<T> cell)
        {
            return gridValidations.All(v => v.IsAvailable(cell));
        }

        public bool IsCellAvailable(Vector3 position)
        {
            var cell = Grid.GetCell(position);
            return IsCellAvailable(cell);
        }

        public T GetValueIfCellIsAvailable(Vector3 position)
        {
            if(IsCellAvailable(position))
                return Grid.GetCell(position).Value;

            return default;
        }

        public WorldGridManager<T> ApplyValidator(params IGridValidation<T>[] gridValidations)
        {
            this.gridValidations = gridValidations.ToList();
            return this;
        }

        public virtual void ResetValidation()
        {
            gridValidations.Clear();
        }
    }
}
