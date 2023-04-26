using UnityEngine;
using UnityFoundation.Code;

namespace UnityFoundation.Cursors
{
    public interface IScreenPositionConverter
    {
        Optional<Vector2> Eval(Vector2 screenPos);
    }
}