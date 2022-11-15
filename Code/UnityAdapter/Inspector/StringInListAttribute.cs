using System;
using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public class StringInListAttribute : PropertyAttribute
    {
        public delegate string[] GetStringList();

        public StringInListAttribute(string[] list)
        {
            List = list;
        }

        public StringInListAttribute(Type type, string methodName)
        {
            var method = type.GetMethod(methodName);
            if(method != null)
            {
                List = method.Invoke(null, null) as string[];
            }
            else
            {
                Debug.LogError("NO SUCH METHOD " + methodName + " FOR " + type);
            }
        }

        public string[] List {
            get;
            private set;
        }
    }
}