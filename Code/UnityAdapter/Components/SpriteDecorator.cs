using UnityEngine;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.Code
{
    public class SpriteDecorator : ISprite
    {
        private readonly UnityObjectRef<Sprite> obj;

        public SpriteDecorator(Sprite sprite)
        {
            obj = new UnityObjectRef<Sprite>(sprite);
        }

        public Vector2 Size => obj.Ref(s => new Vector2(s.texture.width, s.texture.height));

        public Sprite Sprite => obj.Ref(s => s);
    }
}
