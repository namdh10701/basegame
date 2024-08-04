using System;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public List<TimedEvent> timedEvents = new List<TimedEvent>();
    float elapsedTime;
    bool isStarted;
    bool isRunning;
    public Action<float> ElapsedTimeChanged;
    public float timeCap = -1;
    private void Update()
    {
        Tick(Time.deltaTime);
    }
    public void Tick(float deltaTime)
    {
        if (isStarted && isRunning)
        {
            elapsedTime += deltaTime;

            foreach (TimedEvent timedEvent in timedEvents.ToArray())
            {
                if (!timedEvent.IsTriggered)
                {
                    if (elapsedTime > timedEvent.Time)
                    {
                        timedEvent.Action.Invoke();
                        timedEvent.IsTriggered = true;
                        timedEvents.Remove(timedEvent);
                    }
                }
            }
            if (timeCap != -1)
            {
                if (elapsedTime > timeCap)
                {
                    elapsedTime = timeCap;
                    isRunning = false;
                }
            }
            ElapsedTimeChanged?.Invoke(elapsedTime);
        }
    }

    public string TimeString
    {
        get
        {
            string ret = "";
            if (timeCap != -1)
            {
                int minutes = (int)((timeCap - elapsedTime) / 60);
                int seconds = (int)((timeCap - elapsedTime) % 60);
                ret = string.Format("{0:00}:{1:00}", minutes, seconds);
                return ret;
            }
            else
            {
                int minutes = (int)(elapsedTime / 60);
                int seconds = (int)(elapsedTime % 60);
                ret = string.Format("{0:00}:{1:00}", minutes, seconds);
                return ret;
            }
        }
    }

    public void Stop()
    {
        Clear();
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
    public void Clear()
    {
        timedEvents.Clear();
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