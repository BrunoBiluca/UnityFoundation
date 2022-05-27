using UnityEngine;
using UnityEngine.UI;
using UnityFoundation.Code;

namespace UnityFoundation.UI
{
    public class BloodSplatter : MonoBehaviour
    {
        [field: SerializeField] public float DisplayTime { get; private set; }

        private LerpByTime lerp;
        private Image image;

        public BloodSplatter Setup(float displayTime, Vector2 canvasSize)
        {
            var rect = transform.GetComponent<RectTransform>();
            rect.position = new Vector3(
                Random.Range(0, canvasSize.x),
                Random.Range(0, canvasSize.y),
                0f
            );

            DisplayTime = displayTime;
            lerp = new LerpByTime(1f, 0f, DisplayTime); 
            return this;
        }

        public void Awake()
        {
            image = GetComponent<Image>();
            image.material.color = Color.white;
        }

        public void Update()
        {
            if(lerp == null)
                return;

            if(lerp.CurrentValue.NearlyEqual(0f, 0.1f))
            {
                Destroy(gameObject);
                return;
            }
                
            lerp.Eval(Time.deltaTime);
            image.material.color = new Color(1f, 1f, 1f, lerp.CurrentValue);
        }
    }
}