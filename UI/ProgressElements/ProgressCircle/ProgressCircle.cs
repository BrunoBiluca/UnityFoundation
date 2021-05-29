using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UnityFoundation.UI.ProgressElements.ProgressCircle
{
    public class ProgressCircle : MonoBehaviour
    {
        [SerializeField] private float timerMax;
        [SerializeField] private bool loop;
        [SerializeField] private bool destroyWhenFinished;

        private Image mask;
        private float timer;

        void Start()
        {
            mask = transform.Find("mask").GetComponent<Image>();
        }

        public void Setup(float timerMax, bool destroyWhenFinished = false)
        {
            this.timerMax = timerMax;
            this.destroyWhenFinished = destroyWhenFinished;
        }

        private void Update()
        {
            timer += Time.deltaTime;
            mask.fillAmount = timer / timerMax;

            if(timer >= timerMax)
            {
                if(loop)
                {
                    timer = 0f;
                    return;
                }

                if(destroyWhenFinished) Destroy(gameObject);
                else gameObject.SetActive(false);
            }
        }
    }
}