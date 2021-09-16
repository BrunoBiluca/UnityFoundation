using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenIconOptionsMenu : MonoBehaviour
{
    [SerializeField] private List<GameObject> options;

    private Transform optionsHolder;

    private void Awake()
    {
        var horizontalGroup = transform
            .Find("background")
            .Find("options")
            .GetComponent<HorizontalLayoutGroup>();

        if(options.Count < 3)
        {
            horizontalGroup.childControlWidth = false;
            horizontalGroup.childForceExpandWidth = false;
        }
            

        optionsHolder = transform
            .Find("background")
            .Find("options");
    }

    void Start()
    {
        foreach(var option in options)
        {
            Instantiate(option, optionsHolder);
        }
    }
}
