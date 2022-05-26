using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Settings/Option")]
public class SettingsOptionSO : ScriptableObject
{
    public string optionName;
    public object optionValue;

    public EventHandler OnChangeValue;
}
