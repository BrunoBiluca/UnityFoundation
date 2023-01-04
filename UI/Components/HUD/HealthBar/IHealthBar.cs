using System;

namespace UnityFoundation.UI.Components
{
    public interface IHealthBar
    {

        void Setup(float baseHealth);

        void SetCurrentHealth(float currentHealth);

        void Hide() { }

    }
}