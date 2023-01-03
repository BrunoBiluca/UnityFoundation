using UnityFoundation.Code;
using UnityEngine;

namespace UnityFoundation.UI.Components
{
    public class HealthBar : MonoBehaviour, IHealthBar
    {

        private Transform bar;
        private SpriteRenderer barSprite;

        private float maxBarValue;
        private float barSize;
        private float NormalizedBarSize { get { return barSize / maxBarValue; } }

        [SerializeField] private bool notShowWhenFull;

        [SerializeField] private bool useMultipleColors;

        [SerializeField] private Color fullBarColor = new Color32(82, 200, 33, 255);

        [SerializeField] private Color middleBarColor = new Color32(200, 111, 33, 255);

        [SerializeField] private Color lowBarColor = new Color32(200, 33, 40, 255);

        [SerializeField] private GameObject separationTemplate;

        [SerializeField] private int separationValue = 10;

        [SerializeField] private float interpolationTime = 0.05f;

        public void Setup(float baseHealth)
        {
            InitializeComponents();
            maxBarValue = baseHealth;
            SetFull(true);
            InitializeSeparators(baseHealth);
        }

        private void InitializeSeparators(float baseHealth)
        {
            var separatorsHolder = transform.Find("separators_holder");
            TransformUtils.RemoveChildObjects(separatorsHolder);

            if(separationValue == 0) return;

            var separatorCount = Mathf.FloorToInt(baseHealth / separationValue) - 1;
            var separatorDistance = 1f / baseHealth * 2f;
            for(var i = 1; i <= separatorCount; i++)
            {
                var separator = Instantiate(separationTemplate, separatorsHolder);
                separator.name = $"separator_{i}";

                var normalizedPosition = -(1 - i * separatorDistance) / 200f;
                separator.transform.localPosition = new Vector3(normalizedPosition, bar.localPosition.y);
            }
        }

        private void InitializeComponents()
        {
            if(bar != null) return;
            bar = transform.Find("bar");

            if(barSprite != null) return;
            barSprite = bar.Find("barSprite").GetComponent<SpriteRenderer>();
            barSprite.color = fullBarColor;
        }

        internal void SetFull(bool immediately = false)
        {
            SetSize(maxBarValue, immediately);
        }

        public float GetSize() => barSize;

        public void Subtract(float value) => SetCurrentHealth(barSize - value);

        public void SetCurrentHealth(float currentHealth)
        {
            SetSize(currentHealth, false);
        }

        public void SetSize(float size, bool immediately = false)
        {
            barSize = size;

            if(barSize < 0) barSize = 0;
            else if(barSize > maxBarValue) barSize = maxBarValue;

            if(immediately || interpolationTime == 0f)
            {
                BarDimension(NormalizedBarSize);
            }
            else
            {
                InvokeRepeating(nameof(SlowBarReduction), 0f, interpolationTime);
            }
        }

        public void SlowBarReduction()
        {
            try
            {
                if(bar == null)
                {
                    CancelInvoke();
                    return;
                }

                var newBarSize = bar.localScale.x - 0.01f;

                if(newBarSize <= NormalizedBarSize)
                {
                    BarDimension(NormalizedBarSize);

                    if(useMultipleColors) ChangeColor();
                    CancelInvoke();
                    return;
                }

                BarDimension(newBarSize);
            }
            catch(MissingComponentException)
            {
                CancelInvoke();
            }
        }

        private void BarDimension(float size)
        {
            var normalizedPosition = -(1 - size) / 200f;
            bar.localPosition = new Vector3(normalizedPosition, bar.localPosition.y);
            bar.localScale = new Vector3(size, bar.localScale.y);

            BarVisibility();
        }

        private void BarVisibility()
        {
            if(!notShowWhenFull) return;

            if(NormalizedBarSize == 1f) gameObject.SetActive(false);
            else gameObject.SetActive(true);
        }

        private void ChangeColor()
        {
            if(NormalizedBarSize == 1)
                barSprite.color = fullBarColor;
            else if(NormalizedBarSize > .1f && NormalizedBarSize <= .4f)
                barSprite.color = middleBarColor;
            else if(NormalizedBarSize <= .1f)
                barSprite.color = lowBarColor;
        }
    }
}