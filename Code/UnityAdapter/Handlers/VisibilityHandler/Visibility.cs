namespace UnityFoundation.Code.UnityAdapter
{
    public class Visibility : IVisible
    {
        public IGameObject gameObject;

        public bool StartVisible { get; set; }

        public Visibility(IGameObject gameObject)
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
