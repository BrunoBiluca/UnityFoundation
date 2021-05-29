using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDebugUI : MonoBehaviour
{
    Player character;
    TextMeshProUGUI stateText;
    TextMeshProUGUI onGroundText;

    void Start()
    {
        character = GetComponentInParent<Player>();
        stateText = transform.Find("state_text").GetComponent<TextMeshProUGUI>();
        onGroundText = transform.Find("on_ground_text").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        stateText.text = character.CurrentState.GetType().ToString();
        onGroundText.text = character.IsOnGround.ToString();
    }
}
