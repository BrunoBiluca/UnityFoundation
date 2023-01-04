using System;
using System.ComponentModel;
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

        protected override void OnStart()
        {
            worldCursor = worldCursorObj != null
                ? worldCursorObj.GetComponent<IWorldCursor>()
                : GameObjectUtils.FindInScene<IWorldCursor>();

            if(worldCursor == null)
                throw new ArgumentException(
                    $"{nameof(FreeWorldCursorDebug)} not found a {nameof(IWorldCursor)}"
                );

            debugVisual = transform.Find("debug_visual").gameObject;
            debugVisual.SetActive(DebugMode);
        }

        public void Update()
        {
            if(!DebugMode) return;
            if(worldCursor != null) return;

            worldCursor.WorldPosition.Some(pos => {
                debugVisual.SetActive(true);
                transform.position = pos;
            })
            .OrElse(() => debugVisual.SetActive(false));
        }
    }
}