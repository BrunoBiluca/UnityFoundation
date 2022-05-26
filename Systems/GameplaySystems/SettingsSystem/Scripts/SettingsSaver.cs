using UnityFoundation.Code;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsSaver : Singleton<SettingsSaver>
{
    private readonly List<SettingsOptionSO> optionContainers = new List<SettingsOptionSO>();

    public void Save<T>(SettingsOptionSO option, T value)
    {
        option.optionValue = value;

        var optionSaved = optionContainers.Find(oc => oc == option);
        if(optionSaved == null)
            optionContainers.Add(option);
    }

    public bool Load<T>(SettingsOptionSO option, out T value)
    {
        var optionSaved = optionContainers.Find(oc => oc == option);
        if(optionSaved == null)
        {
            value = default;
            return false;
        }   

        value = (T)optionSaved.optionValue;
        return true;
    }

}
