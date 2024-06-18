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
        Debug.Log(" ON A " + crewAction);
        if (actionCoroutine != null)
        {
            StopCoroutine(actionCoroutine);
            if (CurrentAction is not CrewJob)
            {
                Debug.Log(" ON C " + CurrentAction);
            }
            else
            {
                Debug.Log(" ON D " + CurrentAction);
                yield return CurrentAction.Interupt();
            }
        }

        Debug.Log(" ON B " + crewAction);
        CurrentAction = crewAction;
        actionCoroutine = StartCoroutine(ActionCoroutine());
    }
    IEnumerator ActionCoroutine()
    {
        yield return CurrentAction.Execute();
        actionCoroutine = null;
        CurrentAction = null;
        OnFree.Invoke();
    }

}
