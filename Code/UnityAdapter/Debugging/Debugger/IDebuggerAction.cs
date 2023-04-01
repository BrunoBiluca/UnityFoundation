namespace UnityFoundation.Code.DebugHelper
{
    public interface IDebuggerAction
    {
        string Name { get; }
        void Execute();
    }
}
