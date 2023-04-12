using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.Code
{
    public class ParameterNotRegisteredException : Exception
    {
        const string msg 
            = "<type> could not be resolved because parameter <param> was not registered";

        public ParameterNotRegisteredException(Type type, Type parameter)
            : base(
                msg.Replace("<type>", type.ToString())
                  .Replace("<param>", parameter.ToString())
            )
        {
            
        }
    }
}
