using Moq;
using System;
using System.Collections.Generic;

namespace UnityFoundation.TestUtility
{
    public abstract class MockBuilder<TMock> where TMock : class
    {
        private bool WasBuilt = false;
        public Mock<TMock> Mock { get; private set; }

        private readonly ReferenceContainer container = new();

        public TMock Build()
        {
            WasBuilt = true;
            Mock = OnBuild();
            return Mock.Object;
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

        protected abstract Mock<TMock> OnBuild();
    }
}