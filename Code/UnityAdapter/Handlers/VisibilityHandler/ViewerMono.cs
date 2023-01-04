using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public class ViewerMono : MonoBehaviour, IVisible
    {
        private Viewer visibility;

        [field: SerializeField] public bool StartVisible { get; set; }

        public void Awake()
        {
            visibility = new Viewer(gameObject.Decorate()) {
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
