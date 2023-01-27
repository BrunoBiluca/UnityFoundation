namespace UnityFoundation.Code.Tests
{
    public interface IDependencyContainerFixture
    {
        IDependencyContainerFixture WithConstant<T>(T instance);
        IDependencyContainer Build();
        IDependencyContainerFixture Full();
        IDependencyContainerFixture SingleConstructor();
        IDependencyContainerFixture WithDependencySetupInstance();
    }
}
