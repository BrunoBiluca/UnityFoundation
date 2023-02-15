using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.Code
{
    public class ConstantNullException : ArgumentNullException
    {
        public ConstantNullException(Type type) 
            : base($"Constant for {type} should not be null.")
        {
        }
    }
}
