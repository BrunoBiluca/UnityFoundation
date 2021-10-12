using UnityEngine;

namespace Assets.UnityFoundation.SpriteUtils {
    public class SpritePositionSortingOrder : MonoBehaviour {

        [SerializeField]
        private bool runOnce;

        [SerializeField]
        private float positionOffsetY;

        private SpriteRenderer spriteRenderer;

        private void Awake() {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void LateUpdate() {
            var precision = 5f;
            spriteRenderer.sortingOrder = -(int)(
                (transform.position.y - positionOffsetY) * precision
            );

            if(runOnce) {
                Destroy(this);
            }
        }

    }
}