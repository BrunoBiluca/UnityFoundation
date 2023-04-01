using System;

namespace UnityFoundation.Code.Promise
{
    public class Promise
    {
        public static Target Create()
        {
            return new Target(new Promise());
        }

        private Action onSuccess;
        private Action onFail;

        private void Resolve()
        {
            onSuccess?.Invoke();
        }

        private void Reject()
        {
            onFail?.Invoke();
        }

        public Promise Then(Action onSuccess)
        {
            this.onSuccess = onSuccess;
            return this;
        }

        public Promise Catch(Action onFail)
        {
            this.onFail = onFail;
            return this;
        }

        public class Target
        {
            private readonly Promise promise;

            internal Target() { }

            internal Target(Promise promise)
            {
                this.promise = promise;
            }

            public void Resolve()
            {
                promise.Resolve();
            }

            public void Reject()
            {
                promise.Reject();
            }

            public Promise Promise()
            {
                return promise;
            }
        }
    }
}
