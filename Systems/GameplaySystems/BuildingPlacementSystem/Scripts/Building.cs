using UnityFoundation.Code.Grid.ObjectPlacementGrid;
using UnityFoundation.Code.Features;

namespace Assets.UnityFoundation.Systems.BuildingPlacementSystem
{
    public class Building : PooledObject
    {
        public GridObject GridObjectRef { get; private set; }

        public Building Setup(GridObject gridObject)
        {
            GridObjectRef = gridObject;
            return this;
        }
    }
}