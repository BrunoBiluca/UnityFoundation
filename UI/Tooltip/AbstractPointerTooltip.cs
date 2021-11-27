using Assets.UnityFoundation.Code.Common;
using Assets.UnityFoundation.Code.TimeUtils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.UnityFoundation.UI
{
    public abstract class AbstractPointerTooltip :
        MonoBehaviour,
        IPointerEnterHandler,
        IPointerExitHandler
    {
        [SerializeField] protected GameObject tooltipPrefab;
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

        private void Awake()
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