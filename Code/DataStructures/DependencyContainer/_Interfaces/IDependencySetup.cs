namespace UnityFoundation.Code
{
    /// <summary>
    /// Interface used as base for the other IDependencySetup. Defines the interface and method names.
    /// </summary>
    public interface IDependencySetup
    {
        void Setup();
    }

    public interface IDependencySetup<T>
    {
        void Setup(T parameters);
    }

    public interface IDependencySetup<T1, T2>
    {
        void Setup(T1 p1, T2 p2);
    }

    public interface IDependencySetup<T1, T2, T3>
    {
        void Setup(T1 p1, T2 p2, T3 p3);
    }

    public interface IDependencySetup<T1, T2, T3, T4>
    {
        void Setup(T1 p1, T2 p2, T3 p3, T4 p4);
    }
}
