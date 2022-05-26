using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderSettingsOption : MonoBehaviour
{
    [SerializeField] private SettingsOptionSO optionSO;

    private void Awake()
    {
        var slider = transform.Find("slider").GetComponent<Slider>();

        slider.onValueChanged.AddListener(newValue => {
            SettingsSaver.Instance.Save(optionSO, newValue);
            optionSO.OnChangeValue?.Invoke(this, EventArgs.Empty);
        });
    }
}
