using Assets.UnityFoundation.Systems.HealthSystem;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UnityFoundation.Systems.HealthSystem
{
    public class SpriteChangeColor : MonoBehaviour, IHealthBar
    {
        [SerializeField] private List<Color> colors = new List<Color>();

        private SpriteRenderer spriteRenderer;
        private float baseHealth;

        public void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetCurrentHealth(float currentHealth)
        {
            var colorIdx = (int)(baseHealth - currentHealth);

            if(colorIdx >= colors.Count)
                colorIdx = colors.Count - 1;

            spriteRenderer.color = colors[colorIdx];
        }

        public void Setup(float baseHealth)
        {
            this.baseHealth = baseHealth;
        }
    }
}