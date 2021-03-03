using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UnityFoundation.UI.ProgressElements.ProgressCircle {
    public class ProgressCircle : MonoBehaviour {

        private Image mask;
        private float timer;
        private float timerMax;

        void Start() {
            mask = transform.Find("mask").GetComponent<Image>();
        }

        public void Setup(float timerMax) {
            this.timerMax = timerMax;
        }

        private void Update() {
            timer += Time.deltaTime;
            mask.fillAmount = timer / timerMax;

            if(timer >= timerMax) {
                Destroy(gameObject);
            }
        }
    }
}