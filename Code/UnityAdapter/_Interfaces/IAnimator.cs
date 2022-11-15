namespace UnityFoundation.Code.UnityAdapter
{
    public interface IAnimator
    {
        void SetTrigger(string name);
        void SetBool(string name, bool value);
        bool GetBool(string name);
    }
}