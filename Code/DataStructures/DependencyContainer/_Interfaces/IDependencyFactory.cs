using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.Code
{
    public interface IDependencyFactory
    {
        object Instantiate();
    }
}
