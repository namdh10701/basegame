using System.Collections.Generic;
using UnityEngine;

public abstract class ObservedSubject : MonoBehaviour
{
    private List<Observer> observers = new List<Observer>();
    void Notify(Observer observer)
    {
        observer.OnNotified(this);
    }

    public virtual void Attach(Observer observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
        Notify(observer);
    }

    public virtual void Detach(Observer observer)
    {
        if (observers.Contains(observer))
        {
            observers.Remove(observer);
        }
    }

    protected virtual void Notify()
    {
        foreach (Observer observer in observers.ToArray())
        {
            Notify(observer);
        }
    }
}