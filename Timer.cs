using System.Collections;
using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    private bool active = false;
    private float time = 0;

    public bool Active { get => active; set => active = value; }

    private Action onTimeOver;


    // Static method for create a new timer instance
    public static Timer CreateNewTimer(float time, GameObject invoker, Action callback) {
        // Create a new instance and add to the invoker of this timer
        var timerContainer = new GameObject("TimerContainer");
        timerContainer.transform.SetParent(invoker.transform);

        var timer = timerContainer.AddComponent<Timer>();

        // Init the timer
        return timer.Init(time, callback);

    }

    // Init method for initialize the timer with a coroutine
    Timer Init(float time, Action callback) {
        this.time = time;
        this.onTimeOver = callback;
        active = true;

        StartCoroutine(TimerCoroutine());
        return this;
    }

    public Timer RestartTimer(float time, Action callback = null) {
        if (callback != null) callback();

        active = true;
        return Init(time, this.onTimeOver);
    }
    
    IEnumerator TimerCoroutine() {
        yield return new WaitForSeconds(time);

        onTimeOver();
        active = false;
    
    }
}
