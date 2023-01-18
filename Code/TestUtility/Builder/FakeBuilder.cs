using System;

namespace UnityFoundation.TestUtility
{
    public abstract class FakeBuilder<T>
    {
        private bool WasBuilt = false;

        private readonly ReferenceContainer container = new();

        public T Build()
        {
            WasBuilt = true;
            return OnBuild();
        }

        public TObj Get<TObj>()
        {
            return !WasBuilt
                ? throw new InvalidOperationException("Object was not built")
                : container.Get<TObj>();
        }

        public TObj Get<TObj>(string id)
        {
            return !WasBuilt
                ? throw new InvalidOperationException("Object was not built")
                : container.Get<TObj>(id);
        }

        protected string AddToObjects<TObj>(TObj newObject) => container.Add(newObject);

        protected abstract T OnBuild();
    }

}