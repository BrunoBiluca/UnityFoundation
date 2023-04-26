using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityFoundation.Code;

namespace UnityFoundation.SettingsSystem
{
    public class AudioSettingsOptionView : MonoBehaviour
    {
        [SerializeField] private AudioSettingsOptionSO option;

        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private Slider slider;

        [SerializeField] private string labelPath = "background.text";
        [SerializeField] private string sliderPath = "slider";

        private void Awake()
        {
            SetupReferences();
            Display();
        }
        public void SetupReferences()
        {
            label = label != null 
                ? label 
                : transform.FindComponent<TextMeshProUGUI>(labelPath);
            slider = slider != null 
                ? slider 
                : transform.FindComponent<Slider>(sliderPath);
        }

        private void Display()
        {
            label.text = option.OptionName;
            slider.value = option.Value.Volume;
            slider.minValue = option.Value.MetaSettings.MinValue;
            slider.maxValue = option.Value.MetaSettings.MaxValue;
            slider.onValueChanged.AddListener(newValue => option.Value.Volume = newValue);
        }
    }
}