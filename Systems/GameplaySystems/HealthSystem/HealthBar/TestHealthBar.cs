﻿using System;
using UnityEngine;
using UnityEngine.UI;
using UnityFoundation.HealthSystem;

namespace Assets.UnityFoundation.Systems.HealthSystem
{
    public class TestHealthBar : MonoBehaviour {

        [SerializeField] public HealthBar healthBar;

        [SerializeField] public Button clearButton;

        [SerializeField] public InputField barSizeInput;

        void Start() {
            healthBar.SetCurrentHealth(0.3f);

            barSizeInput.onEndEdit.AddListener((inputText) => {
                try {
                    Debug.Log(inputText);
                    var value = float.Parse(inputText);
                    Debug.Log(value);
                    healthBar.Subtract(value);
                } catch(FormatException) {
                    Debug.Log("Formato inválido no campo 'Bar damage'");
                }
            });

            clearButton.onClick.AddListener(() => {
                healthBar.SetFull();
            });
        }

        // Update is called once per frame
        void Update() {

        }


    }
}