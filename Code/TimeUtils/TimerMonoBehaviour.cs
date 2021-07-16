using Assets.UnityFoundation.EditorInspector;
using System;
using UnityEngine;

public class TimerMonoBehaviour : MonoBehaviour
{
    [SerializeField] private bool loop;
    [SerializeField] private bool selfDestroyAfterComplete;
    [SerializeField] private float timerMax;

    [SerializeField]
    [ShowOnly]
    private float timer;
    public float Timer {
        get { return timer; }
        set { timer = value; }
    }

    public bool IsRunning => gameObject.activeInHierarchy;

    private Action callback;

    public void Setup(float amount, Action callback, bool loop = true)
    {
        timer = 0f;
        timerMax = amount;
        this.callback = callback;
        this.loop = loop;

        gameObject.SetActive(true);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Close()
    {
        if(gameObject == null) return;
        Destroy(gameObject);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer < timerMax) return;

        timer = 0f;
        try
        {
            callback();
            if(!loop) Deactivate();
        }
        catch(MissingReferenceException)
        {
            Close();
        }
    }
}