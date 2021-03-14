using UnityEngine;

namespace Assets.UnityFoundation.HealthSystem {
    public class HealthBar : MonoBehaviour, IHealthBar {

        private Transform bar;
        private SpriteRenderer barSprite;

        private float maxBarValue;
        private float barSize;
        private float NormalizedBarSize { get { return barSize / maxBarValue; } }

        [SerializeField]
        private bool notShowWhenFull;

        [SerializeField]
        private bool useMultipleColors;

        [SerializeField]
        private Color fullBarColor = new Color32(82, 200, 33, 255);

        [SerializeField]
        private Color middleBarColor = new Color32(200, 111, 33, 255);

        [SerializeField]
        private Color lowBarColor = new Color32(200, 33, 40, 255);

        public void Setup(float maxValue) {
            InitializeComponents();
            maxBarValue = maxValue;
            SetFull(true);
        }

        private void InitializeComponents() {
            if(bar != null) return;
            bar = transform.Find("bar");

            if(barSprite != null) return;
            barSprite = bar.Find("barSprite").GetComponent<SpriteRenderer>();
            barSprite.color = fullBarColor;
        }

        internal void SetFull(bool immediately = false) {
            SetSize(maxBarValue, immediately);
        }

        public float GetSize() => barSize;

        public void Subtract(float value) => SetSize(barSize - value);

        public void SetSize(float size) {
            SetSize(size, false);
        }

        public void SetSize(float size, bool immediately = false) {
            barSize = size;

            if(barSize < 0) barSize = 0;
            else if(barSize > maxBarValue) barSize = maxBarValue;

            if(immediately) {
                BarDimension(NormalizedBarSize);
            } else {
                InvokeRepeating(nameof(SlowBarReduction), 0f, 0.05f);
            }
        }

        public void SlowBarReduction() {
            var newBarSize = bar.localScale.x - 0.01f;

            if(newBarSize <= NormalizedBarSize) {
                BarDimension(NormalizedBarSize);

                if(useMultipleColors) ChangeColor();
                CancelInvoke();
                return;
            }

            BarDimension(newBarSize);
        }

        private void BarDimension(float size) {
            var normalizedPosition = -(1 - size) / 2f;
            bar.localPosition = new Vector3(normalizedPosition, bar.localPosition.y);
            bar.localScale = new Vector3(size, bar.localScale.y);

            BarVisibility();
        }

        private void BarVisibility() {
            if(!notShowWhenFull) return;

            if(NormalizedBarSize == 1f) gameObject.SetActive(false);
            else gameObject.SetActive(true);
        }

        private void ChangeColor() {
            if(NormalizedBarSize == 1)
                barSprite.color = fullBarColor;
            else if(NormalizedBarSize > .1f && NormalizedBarSize <= .4f)
                barSprite.color = middleBarColor;
            else if(NormalizedBarSize <= .1f)
                barSprite.color = lowBarColor;
        }
    }
}