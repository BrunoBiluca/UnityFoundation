using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public class VisibilityMono : MonoBehaviour, IVisible
    {
        private Visibility visibility;

        [field: SerializeField] public bool StartVisible { get; set; }

        public void Awake()
        {
            visibility = new Visibility(gameObject.Decorate()) {
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
