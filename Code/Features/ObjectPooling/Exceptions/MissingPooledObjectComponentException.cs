using UnityEditor;
using UnityEngine;
using static UnityFoundation.Code.Features.ObjectPoolingStrings;

namespace UnityFoundation.Code
{
    public class MissingPooledObjectComponentException : MissingComponentException
    {
        public MissingPooledObjectComponentException()
            : base(MISSING_POOLED_OBJECT_COMPONENT_MSG())
        { }

        public MissingPooledObjectComponentException(string objectName)
            : base(MISSING_POOLED_OBJECT_COMPONENT_MSG(objectName))
        { }
    }
}
