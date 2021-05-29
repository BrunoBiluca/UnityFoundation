using System;
using UnityEngine;

public class TimerV2
{
    private static GameObject timersReference;

    private static void TryGetTimersReference()
    {
        if(timersReference != null) return;

        var timersRef = GameObject.Find("** Timers");
        if(timersRef == null)
        {
            timersRef = new GameObject("** Timers");
        }

        timersReference = timersRef;
    }

    /// <summary>
    /// Get the time passed in the current loop in seconds
    /// </summary>
    public float CurrentTime { get { return timerBehaviour.Timer; } }

    /// <summary>
    /// Get the time to end the current loop in seconds
    /// </summary>
    public float RemainingTime { get { return amount - timerBehaviour.Timer; } }

    /// <summary>
    /// Get the completion percentage of the timer
    /// </summary>
    public float Completion { get { 
            return CurrentTime / amount * 100f; 
        } 
    }

    private TimerMonoBehaviour timerBehaviour;
    private string name;
    private readonly float amount;
    private bool runOnce;
    private readonly Action callback;

    /// <summary>
    /// Instantiate a gameobject to run the timer for some provider action, by default run once and stop
    /// </summary>
    /// <param name="amount">time in seconds</param>
    /// <param name="callback">callback called when amount of time is reached</param>
    public TimerV2(float amount, Action callback)
    {
        this.amount = amount;
        this.callback = callback;
        runOnce = true;
    }

    public TimerV2 SetName(string name)
    {
        this.name = name;
        return this;
    }

    public TimerV2 RunOnce()
    {
        runOnce = true;
        return this;
    }

    public TimerV2 Loop()
    {
        runOnce = false;
        return this;
    }

    /// <summary>
    /// Start the timer with the setting parameters
    /// </summary>
    public TimerV2 Start()
    {
        if(timerBehaviour == null)
            InstantiateTimer();

        timerBehaviour.Setup(amount, callback, runOnce);

        return this;
    }

    /// <summary>
    /// Stop running the timer
    /// </summary>
    public void Stop()
    {
        if(timerBehaviour == null) return;
        timerBehaviour.Close();
    }

    private void InstantiateTimer()
    {
        TryGetTimersReference();

        if(string.IsNullOrEmpty(name)) name = Guid.NewGuid().ToString();

        timerBehaviour = new GameObject(name).AddComponent<TimerMonoBehaviour>();
        timerBehaviour.transform.SetParent(timersReference.transform);
    }
}
