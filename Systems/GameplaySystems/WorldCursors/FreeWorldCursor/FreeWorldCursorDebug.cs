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

        protected override void OnAwake()
        {
            GetWorldCursorRef();
            debugVisual = transform.Find("debug_visual").gameObject;
            debugVisual.SetActive(DebugMode);
        }

        private void GetWorldCursorRef()
        {
            worldCursor = worldCursorObj != null
                ? worldCursorObj.GetComponent<IWorldCursor>()
                : GameObjectUtils.FindInScene<IWorldCursor>();
        }

        public void Update()
        {
            if(!DebugMode) return;
            if(worldCursor == null)
            {
                GetWorldCursorRef();
                return;
            }

            worldCursor.WorldPosition.Some(pos => {
                debugVisual.SetActive(true);
                debugVisual.transform.position = pos;
            })
            .OrElse(() => debugVisual.SetActive(false));
        }
    }
}