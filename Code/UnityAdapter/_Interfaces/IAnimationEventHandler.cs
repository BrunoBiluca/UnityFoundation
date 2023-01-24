namespace UnityFoundation.Code.Extensions
{
    public interface IAnimationEventHandler
    {
        void AnimationEventHandler(string eventName);
    }

    public interface IAnimationEventHandler<T>
    {
        void AnimationEventHandler(T value);
    }
}
