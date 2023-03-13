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

        public static ICollider Decorate(this CapsuleCollider collider)
            => new CapsuleColliderDecorator(collider);

        public static ICamera Decorate(this Camera camera)
            => new CameraDecorator(camera);
    }
}
