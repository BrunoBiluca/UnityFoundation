using Moq;
using System;
using System.Collections.Generic;

namespace UnityFoundation.TestUtility
{
    public abstract class MockBuilder<TMock> where TMock : class
    {
        private bool WasBuilt = false;
        public Mock<TMock> Mock { get; private set; }

        private readonly Dictionary<Type, object> innerObjects = new();

        public T Get<T>()
        {
            if(!WasBuilt) throw new InvalidOperationException("Builder was not built");
            return (T)innerObjects[typeof(T)];
        }

        public TMock Build()
        {
            WasBuilt = true;
            Mock = OnBuild();
            return Mock.Object;
        }

        protected void AddToObjects<T>(T newObject)
        {
            innerObjects.Add(typeof(T), newObject);
        }

        protected abstract Mock<TMock> OnBuild();
    }
}