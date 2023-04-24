using System;

namespace UnityFoundation.UI.ViewSystem
{
    public class CallOnce
    {
        private readonly Action callback;
        private bool wasCalled;

        public CallOnce(Action callback)
        {
            this.callback = callback;
        }

        public void Call()
        {
            if(wasCalled) return;
            wasCalled = true;
            callback();
        }
    }
}