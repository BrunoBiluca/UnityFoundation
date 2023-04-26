using Moq;
using NUnit.Framework;
using UnityEngine;

namespace UnityFoundation.Code.Tests
{
    public class UpdateProcessorTests
    {
        [Test]
        public void Should_register_updatable_entity()
        {
            var updatable = new Mock<IUpdatable>();

            var processor = new GameObject("update_processor").AddComponent<UpdateProcessor>();
            processor.Register(updatable.Object);

            processor.Update();

            updatable.Verify(u => u.Update(It.IsAny<float>()), Times.Once());
        }
    }
}