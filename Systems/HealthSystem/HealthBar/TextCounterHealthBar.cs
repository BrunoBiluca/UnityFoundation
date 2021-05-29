using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextCounterHealthBar : MonoBehaviour, IHealthBar {

    [SerializeField]
    private TMP_Text text;

    private float baseHealth;
    private float currentHealth;

    void Awake() {
        if(text == null) {
            text = transform.Find("text").GetComponent<TMP_Text>();
        }
    }

    public void Setup(float baseHealth) {
        this.baseHealth = baseHealth;
        this.currentHealth = baseHealth;
        text.text = Mathf.FloorToInt(this.currentHealth).ToString();
    }

    public void SetCurrentHealth(float currentHealth) {
        this.currentHealth = Mathf.Clamp(currentHealth, 0, baseHealth);
        text.text = Mathf.FloorToInt(this.currentHealth).ToString();
    }
}
