using _Game.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace _Game.Features.Gameplay
{
    public class CrewActionHandler : MonoBehaviour
    {
        public Crew crew;
        public CrewActionBase currentAction;
        public IEnumerator Act(Queue<CrewActionBase> actionQueue)
        {
            while (actionQueue.Count > 0)
            {
                CrewActionBase action = actionQueue.Dequeue();
                currentAction = action;
                Debug.Log(action + " P");
                yield return action.Execute();
            }
            currentAction = null;
        }

        public IEnumerator Act(CrewActionBase crewAction)
        {
            Debug.Log(crewAction + " A");
            currentAction = crewAction;
            yield return crewAction.Execute();
            currentAction = null;
        }

        public void InteruptCurrentAction()
        {
            if (currentAction != null) { 
            Debug.Log("INTERUPT" + currentAction);
            currentAction.Interupt();
            }

        }

    }
}