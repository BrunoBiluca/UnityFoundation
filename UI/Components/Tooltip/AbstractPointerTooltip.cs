using UnityFoundation.Code;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityFoundation.Code.Timer;

namespace UnityFoundation.UI
{
    public abstract class AbstractPointerTooltip :
        MonoBehaviour,
        IPointerEnterHandler,
        IPointerExitHandler
    {
        [SerializeField] protected GameObject tooltipPrefab;
        [field: SerializeField] public Transform TooltipTarget { get; set; }
        [SerializeField] private float tooltipScreenTimer = 0;

        private GameObject tooltipGO;
        private Optional<Timer> tooltipTimer;

        protected abstract void UpdateTooltip(GameObject tooltipGO);

        protected virtual Optional<Timer> SetTimer()
        {
            if(tooltipScreenTimer > 0f)
                return Optional<Timer>.Some(new Timer(tooltipScreenTimer, Deactivate));
            else
                return Optional<Timer>.None();
        }

        protected virtual void SetupTooltipObject(GameObject tooltipObject) { }

        public void Awake()
        {
            tooltipGO = Instantiate(
                tooltipPrefab,
                GetComponentInParent<Canvas>().transform
            );
            tooltipTimer = SetTimer();
            SetupTooltipObject(tooltipGO);

            Deactivate();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            tooltipTimer.Some(timer => timer.Start());

            UpdateTooltip(tooltipGO);

            if(TooltipTarget != null)
                tooltipGO.transform.position = TooltipTarget.position;
            else 
                tooltipGO.transform.position = transform.position;

            tooltipGO.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Deactivate();
        }

        private void Deactivate()
        {
            tooltipGO.SetActive(false);
        }

        private void OnDestroy()
        {
            Destroy(tooltipGO);
        }
    }
}