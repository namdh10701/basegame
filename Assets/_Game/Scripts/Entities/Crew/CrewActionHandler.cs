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

    public void Act(CrewAction crewAction)
    {
        StartCoroutine(HandleAssignNewAction(crewAction));
    }

    IEnumerator HandleAssignNewAction(CrewAction crewAction)
    {
        if (actionCoroutine != null)
        {
            StopCoroutine(actionCoroutine);
            yield return CurrentAction.Interupt();
        }
        CurrentAction = crewAction;
        actionCoroutine = StartCoroutine(ActionCoroutine());
    }
    IEnumerator ActionCoroutine()
    {
        yield return CurrentAction.Execute();
        actionCoroutine = null;
        OnFree.Invoke();
    }

}
