using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    public List<TimedEvent> timedEvents = new List<TimedEvent>();
    float elapsedTime;
    bool isStarted;
    bool isRunning;

    private void Update()
    {
        if (isStarted && isRunning)
        {
            elapsedTime += Time.deltaTime;
            foreach (TimedEvent timedEvent in timedEvents)
            {
                if (!timedEvent.IsTriggered)
                {
                    if (elapsedTime > timedEvent.Time)
                    {
                        timedEvent.Action.Invoke();
                        timedEvent.IsTriggered = true;
                    }
                }
            }
        }
    }

    public void Stop()
    {
        isStarted = false;
        elapsedTime = 0;
    }
    public void Pause()
    {
        isRunning = false;
    }
    public void Resume()
    {
        isRunning = true;
    }
    public void StartTimer()
    {
        isStarted = true;
        isRunning = true;
        elapsedTime = 0;
    }

    public void SetTime(float time)
    {
        elapsedTime = time;
    }


    public void RegisterEvent(TimedEvent timedEvent)
    {
        if (!timedEvents.Contains(timedEvent))
        {
            timedEvents.Add(timedEvent);
        }
    }

    public void UnregisterEvent(TimedEvent timedEvent)
    {
        if (timedEvents.Contains(timedEvent))
        {
            timedEvents.Remove(timedEvent);
        }
    }
}