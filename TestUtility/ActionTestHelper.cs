using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.TestUtility
{
    public sealed class ActionTestHelper
    {
        public int TimesExecuted { get; private set; }
        public bool WasExecuted { get; private set; }

        public Action Action { get; private set; }

        public ActionTestHelper()
        {
            TimesExecuted = 0;
            WasExecuted = false;
            Action = () => {
                TimesExecuted++;
                WasExecuted = true;
            };
        }
    }
}