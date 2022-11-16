using UnityEngine;
using UnityFoundation.Code;

namespace UnityFoundation.WorldCursors
{
    public class FreeWorldCursorDebug : Singleton<FreeWorldCursorDebug>
    {
        private IWorldCursor worldCursor;
        private GameObject debugVisual;

        [SerializeField] private GameObject worldCursorObj;
        [field: SerializeField] private bool DebugMode { get; set; }

        public void Start()
        {
            worldCursor = worldCursorObj.GetComponent<IWorldCursor>();

            debugVisual = transform.Find("debug_visual").gameObject;
            debugVisual.SetActive(DebugMode);
        }

        public void Update()
        {
            if(worldCursor == null) return;

            if(DebugMode)
            {
                worldCursor.WorldPosition.Some(pos => {
                    debugVisual.SetActive(true);
                    transform.position = pos;
                })
                .OrElse(() => debugVisual.SetActive(false));
            }
        }
    }
}