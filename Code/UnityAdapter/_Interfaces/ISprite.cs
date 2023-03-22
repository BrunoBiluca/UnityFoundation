using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public interface ISprite
    {
        Vector2 Size { get; }
        Sprite Sprite { get; }
    }
}
