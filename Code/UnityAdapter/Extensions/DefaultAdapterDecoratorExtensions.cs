using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public static class DefaultAdapterDecoratorExtensions
    {
        public static IGameObject Decorate(this GameObject gameObject)
            => new GameObjectDecorator(gameObject);

        public static ITransform Decorate(this Transform transform)
            => new TransformDecorator(transform);

        public static IAudioSource Decorate(this AudioSource source) 
            => new AudioSourceDec(source);
    }
}
