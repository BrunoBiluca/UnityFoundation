using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public interface ICamera
    {
        Vector2 ScreenCenter();
        Ray ScreenPointToRay(Vector2 position);
    }
}
