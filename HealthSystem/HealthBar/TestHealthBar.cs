using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UnityFoundation.HealthSystem {
    public class TestHealthBar : MonoBehaviour {

        [SerializeField] public HealthBar healthBar;

        [SerializeField] public Button clearButton;

        [SerializeField] public InputField barSizeInput;

        void Start() {
            healthBar.SetSize(0.3f);

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