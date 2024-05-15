using System;
public class TimedEvent
{
    public int Id;
    public float Time;
    public Action Action;
    public bool IsTriggered;

    public TimedEvent(float time, Action action)
    {
        this.Time = time;
        this.Action = action;
    }
}
