using System;
using System.Collections.Generic;

namespace UnityFoundation.TestUtility
{
    public sealed class ReferenceContainer
    {
        private readonly Dictionary<string, object> innerObjects = new();

        public TObj Get<TObj>()
        {
            foreach(var obj in innerObjects)
                if(obj.Value is TObj obj1)
                    return obj1;

            return default;
        }

        public TObj Get<TObj>(string id)
        {
            return (TObj)innerObjects[id];
        }

        public string Add<TObj>(TObj newObject)
        {
            var id = Guid.NewGuid().ToString();
            innerObjects.Add(id, newObject);
            return id;
        }
    }

}