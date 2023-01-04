using UnityEngine;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.Code
{
    public static class DefaultAdapterDecoratorExtensions
    {
        public static IGameObject Decorate(this GameObject gameObject) 
            =>  new GameObjectDecorator(gameObject);

        public static ITransform Decorate(this Transform transform)
            => new TransformDecorator(transform);
    }
}
