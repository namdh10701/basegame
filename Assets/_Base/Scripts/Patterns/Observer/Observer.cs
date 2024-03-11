using System;
using UnityEngine;
public abstract class Observer : MonoBehaviour
{
    protected ObservedSubject observedSubject;
    private void OnEnable()
    {
        observedSubject?.Attach(this);
    }
    private void OnDisable()
    {
        observedSubject?.Detach(this);
    }
    public abstract void OnNotified(ObservedSubject observedSubject);
}
