namespace UnityFoundation.Code
{
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
