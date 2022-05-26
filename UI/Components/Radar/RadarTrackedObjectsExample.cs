using UnityEditor.Experimental.SceneManagement;
using UnityEngine;

namespace UnityFoundation.Radar
{
    [ExecuteInEditMode]
    public class RadarTrackedObjectsExample : MonoBehaviour
    {
        private RadarView radar;
        private RectTransform[] objectReferences;
        private Vector3[] originalPositions;

        public void Awake()
        {
            Init();
        }

        private void Init()
        {
            radar = GetComponentInParent<RadarView>();

            objectReferences = new RectTransform[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                objectReferences[i] = transform.GetChild(i).GetComponent<RectTransform>();
            }
            
            originalPositions = new Vector3[objectReferences.Length];
            for(int i = 0; i < objectReferences.Length; i++)
            {
                originalPositions[i] = new Vector3(
                    Random.Range(-40f, 40f),
                    Random.Range(-40f, 40f),
                    0
                );
            }
        }

        public void Update()
        {
            if(PrefabStageUtility.GetCurrentPrefabStage() == null)
            {
                gameObject.SetActive(false);
            }
                    
            if(radar == null)
            {
                Init();
            }

            for(int i = 0; i < objectReferences.Length; i++)
            {
                objectReferences[i].localPosition = originalPositions[i] * radar.MapScale;
            }
        }
    }
}