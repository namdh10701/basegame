using System;
using System.Collections;
using System.Collections.Generic;

public class BaseGameEvent
{
    readonly List<IGameEventListener> m_EventListeners = new List<IGameEventListener>();

    public void Raise()
    {
        foreach (var listener in m_EventListeners)
        {
            listener.OnEventRaised();
        }
        Reset();
    }

    public void AddListener(IGameEventListener listener)
    {
        if (!m_EventListeners.Contains(listener))
        {
            m_EventListeners.Add(listener);
        }
    }

    public void RemoveListener(IGameEventListener listener)
    {
        m_EventListeners.Remove(listener);
    }

    public virtual void Reset()
    {
        // Reset logic for the derived classes.
    }
}