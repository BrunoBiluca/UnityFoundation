using Moq;
using UnityEngine;
using UnityFoundation.Code.UnityAdapter;
using UnityFoundation.TestUtility;

namespace UnityFoundation.Code.Tests
{
    public class RaycastHandlerMockBuilder : MockBuilder<IRaycastHandler>
    {
        private readonly Mock<IRaycastHandler> raycastHandler;
        public RaycastHandlerMockBuilder()
        {
            raycastHandler = new Mock<IRaycastHandler>();
        }

        public RaycastHandlerMockBuilder WithReturnedObject<T>() where T : class
        {
            var expectedObject = new Mock<T>();
            raycastHandler
                .Setup(h => h.GetObjectOf<T>(It.IsAny<Vector2>(), It.IsAny<LayerMask>()))
                .Returns(expectedObject.Object);

            return this;
        }

        protected override Mock<IRaycastHandler> OnBuild()
        {
            return raycastHandler;
        }
    }
}