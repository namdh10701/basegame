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

        public void Act(CrewActionBase crewAction)
        {
            actionCoroutine = StartCoroutine(ActionCoroutine());
        }

        IEnumerator ActionCoroutine()
        {
            yield return CurrentAction.Execute;
            actionCoroutine = null;
            CurrentAction = null;
            OnFree?.Invoke();
        }

        void OnChangedStatus(JobStatus jobStatus)
        {
            if (jobStatus == JobStatus.Deactive)
            {
                StopCoroutine(actionCoroutine);
                CurrentAction.Interupt();
            }
        }

        public void CancelCurrentAction()
        {
            if (CurrentAction != null)
            {
                CurrentAction.Interupt();
                if (actionCoroutine != null)
                {
                    StopCoroutine(actionCoroutine);
                    actionCoroutine = null;
                }
            }
        }

        public void PauseCurrentAction()
        {
            if (actionCoroutine != null)
            {
                StopCoroutine(actionCoroutine);
            }
        }

        public void ResumeCurrentAction()
        {
            if (CurrentAction != null)
            {
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