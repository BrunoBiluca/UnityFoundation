using System;

namespace UnityFoundation.UI.ViewSystem
{
    public class CallOnce
    {
        private readonly Action callback;
        public bool WasCalled { get; private set; }

        public CallOnce(Action callback)
        {
            this.callback = callback;
        }

        public void Call()
        {
            if(WasCalled) return;
            WasCalled = true;
            callback();
        }
    }
}