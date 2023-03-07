namespace UnityFoundation.Code.Timer
{
    public interface ITimer
    {
        bool Completed { get; }
        float Completion { get; }
        float CurrentTime { get; }
        bool IsRunning { get; }
        float RemainingTime { get; }

        void Close();
        ITimer Loop();
        void Resume();
        ITimer RunOnce();
        ITimer SetAmount(float newAmount);
        ITimer SetName(string name);
        ITimer Start();
        void Stop();
    }
}