using UnityFoundation.Code;

namespace UnityFoundation.Code.UnityAdapter
{
    public class VisibilityHandler : IVisible
    {
        public IGameObject gameObject;

        public bool StartVisible { get; set; }

        public VisibilityHandler(IGameObject gameObject)
        {
            this.gameObject = gameObject;

            if(StartVisible) Show();
            else Hide();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
