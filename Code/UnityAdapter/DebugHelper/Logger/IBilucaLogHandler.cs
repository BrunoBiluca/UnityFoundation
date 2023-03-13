namespace UnityFoundation.Code
{
    public interface IBilucaLogHandler
    {
        void Log(string message);
        void Warn(string message);
        void Error(string message);
    }
}
