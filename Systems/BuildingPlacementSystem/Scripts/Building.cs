using Assets.UnityFoundation.Code.Grid.ObjectPlacementGrid;
using Assets.UnityFoundation.Code.ObjectPooling;

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