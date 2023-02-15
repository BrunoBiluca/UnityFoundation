namespace UnityFoundation.Code.UnityAdapter
{
    public interface IVisible
    {
        bool StartVisible { get; set; }
        void Hide();
        void Show();
    }
}
