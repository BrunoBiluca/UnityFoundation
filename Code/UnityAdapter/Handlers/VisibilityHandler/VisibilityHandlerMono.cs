using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public class VisibilityHandlerMono : MonoBehaviour, IVisible
    {
        private VisibilityHandler visibility;

        [field: SerializeField] public bool StartVisible { get; set; }

        public void Awake()
        {
            visibility = new VisibilityHandler(
                new GameObjectDecorator(gameObject)
            ) {
                StartVisible = StartVisible
            };
        }

        public void Hide()
        {
            visibility.Hide();
        }

        public void Show()
        {
            visibility.Show();
        }
    }
}
