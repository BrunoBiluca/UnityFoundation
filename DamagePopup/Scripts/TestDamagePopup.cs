using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UnityFoundation.DamagePopup.Scripts {
    public class TestDamagePopup : MonoBehaviour {

        void Update() {
            if(Input.GetMouseButtonDown(0)) {
                var mouse = Input.mousePosition;
                var worldPosition = Camera.main.ScreenToWorldPoint(mouse);

                var damage = Random.Range(0, 100);
                var isCriticalHit = damage >= 80;

                if(isCriticalHit)
                    DamagePopup.CreateCritical(damage.ToString(), worldPosition);
                else
                    DamagePopup.Create(damage.ToString(), worldPosition);
            }

        }
    }
}