using UnityEngine;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.Code.Grid
{
    public abstract class WorldGridXZMono<T> : MonoBehaviour, IGridXZBase
    {
        public IWorldGridXZ<T> Grid { get; private set; }

        [field: SerializeField] public GridXZConfig Config { get; private set; }

        public void Awake()
        {
            if(Grid == null)
                Setup(Config);
        }

        public void Setup(GridXZConfig config)
        {
            Grid = new WorldGridXZ<T>(
                transform.position,
                config.Width,
                config.Depth,
                config.CellSize,
                () => InstantiateValue()
            );
        }

        protected abstract T InstantiateValue();
    }
}
