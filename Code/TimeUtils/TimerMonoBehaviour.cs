using Assets.UnityFoundation.EditorInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerMonoBehaviour : MonoBehaviour
{
    [SerializeField]
    private bool runOnce;

    [SerializeField]
    private float timerMax;

    [SerializeField]
    [ReadOnlyInspector]
    private float timer;
    public float Timer {
        get { return timer; }
        set { timer = value; }
    }
    private Action callback;

    public void Setup(float amount, Action callback, bool runOnce = true)
    {
        timer = 0f;
        timerMax = amount;
        this.callback = callback;
        this.runOnce = runOnce;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer < timerMax) return;

        timer = 0f;
        try
        {
            callback();
            if(runOnce) Close();
        }
        catch(MissingReferenceException)
        {
            Close();
        }
    }

    public void Close()
    {
        if(gameObject == null) return;
        Destroy(gameObject);
    }
}