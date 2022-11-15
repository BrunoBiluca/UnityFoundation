using UnityEngine;

namespace UnityFoundation.Code.Grid
{
    public abstract class WorldGridXZMono<T> : MonoBehaviour
    {
        public IWorldGridXZ<T> Grid { get; private set; }

        [SerializeField] private GridXZConfig config;

        public void Awake()
        {
            if(Grid == null)
                Setup(config);
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
