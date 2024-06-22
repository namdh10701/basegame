using _Game.Scripts;
using _Game.Scripts.CrewSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewActionHandler : MonoBehaviour
{
    Coroutine actionCoroutine;
    public CrewAction CurrentAction;
    public Action OnFree;
    public bool isPaused;
    public void Act(CrewAction crewAction)
    {
        StartCoroutine(HandleAssignNewAction(crewAction));
    }

    IEnumerator HandleAssignNewAction(CrewAction crewAction)
    {
        if (isPaused)
        {
            yield break; // Exit coroutine if paused
        }
        if (actionCoroutine != null)
        {
            StopCoroutine(actionCoroutine);
           
            if (CurrentAction is not CrewJobAction)
            {
            }
            else
            {
                yield return CurrentAction.Interupt;
            }
        }
        CurrentAction = crewAction;
        actionCoroutine = StartCoroutine(ActionCoroutine());
    }
    IEnumerator ActionCoroutine()
    {
        yield return CurrentAction.Execute;
        actionCoroutine = null;
        CurrentAction = null;
        OnFree.Invoke();
    }

    public void Pause()
    {
        isPaused = true;
        if (actionCoroutine != null)
        {
            StopCoroutine(actionCoroutine);
        }
    }

    public void Resume()
    {
        isPaused = false;
        if (CurrentAction != null)
        {
            actionCoroutine = StartCoroutine(ActionCoroutine());
        }
    }

}
