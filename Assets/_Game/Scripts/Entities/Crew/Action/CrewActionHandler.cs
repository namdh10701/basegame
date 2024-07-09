using _Game.Scripts;
using System;
using System.Collections;
using UnityEngine;
namespace _Game.Features.Gameplay
{
    public class CrewActionHandler : MonoBehaviour
    {
        public Crew crew;
        Coroutine actionCoroutine;
        public CrewActionBase CurrentAction;
        public Action OnFree;
        bool isPaused;

        public void Act(CrewActionBase crewAction)
        {
            StartCoroutine(HandleAssignNewAction(crewAction));
        }

        IEnumerator HandleAssignNewAction(CrewActionBase crewAction)
        {
            if (isPaused)
            {
                yield break; // Exit coroutine if paused
            }
            if (actionCoroutine != null)
            {
                StopCoroutine(actionCoroutine);
                crew.CrewMovement.Velocity = Vector2.zero;
                if (CurrentAction is not CrewJobAction)
                {
                }
                else
                {
                    CurrentAction.Interupt();
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
                StopCoroutine(CurrentAction.Execute);
                StopCoroutine(actionCoroutine);
            }
        }

        public void Resume()
        {
            isPaused = false;
            if (CurrentAction != null)
            {
                CurrentAction.Interupt();
                CurrentAction.ReBuild(crew);
                actionCoroutine = StartCoroutine(ActionCoroutine());
            }
            else
            {
                OnFree?.Invoke();
            }
        }
    }
}