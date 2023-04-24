namespace UnityFoundation.UI.ViewSystem
{
    public class ExclusiveViewGroup
    {
        private IView main;
        private ViewsGroup otherViews = new();

        public void RegisterMain(IView view)
        {
            main = view;
            main.OnVisible += HandleVisible;
        }

        private void HandleVisible()
        {
            otherViews.Hide();
        }

        public void Register(IView view)
        {
            view.CanBeShow += CanShowOtherView;
            otherViews.Register(view);

            if(main.IsVisible)
                otherViews.Hide();
        }

        private bool CanShowOtherView() => !main.IsVisible;

        public void ShowMain()
        {
            main.Show();
        }

        public void HideMain()
        {
            main.Hide();
        }
    }
}