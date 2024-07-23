using _Game.Scripts;
using _Game.Scripts.Entities;
using _Game.Scripts.PathFinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.CullingGroup;

namespace _Game.Features.Gameplay
{
    public enum CrewActionState
    {
        Free, Working
    }
    public class CrewAction : MonoBehaviour
    {

        public CrewActionState state;
        public CrewActionState State
        {
            get => state; set
            {
                CrewActionState lastState = state;

                state = value;
                if (state != lastState)
                {
                    OnStateChanged.Invoke(value);
                }
            }
        }

        public Action<CrewActionState> OnStateChanged;

        [SerializeField] Crew crew;
        CrewController crewController;
        [SerializeField] CrewActionHandler Handler;

        public CrewTask DoingTask;
        Idle idle;
        Wander wander;
        bool isActive;
        public SpriteRenderer carryObject;
        private Ammo carryingBullet;
        MoveData moveData;

        public Ammo CarryingBullet
        {
            get
            {
                return carryingBullet;
            }
            set
            {
                carryingBullet = value;
                if (value != null)
                    carryObject.sprite = value.Def.ProjectileImage;
                carryObject.gameObject.SetActive(value != null);
            }
        }

        private void Start()
        {
            moveData = FindAnyObjectByType<MoveData>();
            crewController = FindAnyObjectByType<CrewController>();
            idle = new Idle(crew, crew.OccupyingNodes[0]);
            DoAction(idle);
        }

        void OnFree()
        {
            if (!isActive || crewController == null)
            {
                DoAction(new Idle(crew, crew.OccupyingNodes[0]));
                return;
            }
            DoingTask = null;
            if (crewController.GetMostSuitableTaskForCrew(crew, out CrewTask task))
            {
                DoTask(task);
            }
            else
            {
                float rand = UnityEngine.Random.Range(0f, 1f);
                if (rand < 0.35f)
                {
                    DoAction(new Idle(crew, crew.OccupyingNodes[0]));
                }
                else
                {
                    DoAction(new Wander(crew, moveData));
                }
            }
        }
        bool isPause;
        public void Pause()
        {
            isPause = true;
            if (actionCoroutine != null)
            {
                StopCoroutine(actionCoroutine);
                Handler.InteruptCurrentAction();
            }
        }
        public void Resume()
        {
            isPause = false;
            if (DoingTask != null)
            {
                actionCoroutine = StartCoroutine(DoTaskCoroutine(DoingTask));
            }
        }
        public void Activate()
        {
            isActive = true;
        }
        public void Deactivate()
        {
            isActive = false;
        }
        Coroutine actionCoroutine;

        public void DoAction(CrewActionBase action)
        {
            if (isPause)
            {
                return;
            }
            if (actionCoroutine != null)
            {
                StopCoroutine(actionCoroutine);
                Handler.InteruptCurrentAction();

            }
            actionCoroutine = StartCoroutine(DoActionCoroutine(action));
        }

        IEnumerator DoActionCoroutine(CrewActionBase action)
        {
            yield return Handler.Act(action);
            actionCoroutine = null;
            OnFree();
        }



        public void DoTask(CrewTask task)
        {
            if (DoingTask == task)
            {
                Debug.Log("RETURN");
                return;
            }
            if (DoingTask != null)
            {
                DoingTask.Status = TaskStatus.Pending;
            }
            if (actionCoroutine != null)
            {
                Debug.Log("stop coroutine");
                StopCoroutine(actionCoroutine);
                Handler.InteruptCurrentAction();
            }
            
            Debug.Log(task);
            DoingTask = task;
            if (isPause)
            {
                return;
            }
            actionCoroutine = StartCoroutine(DoTaskCoroutine(task));

        }

        IEnumerator DoTaskCoroutine(CrewTask task)
        {
            DoingTask.Status = TaskStatus.Working;
            task.RegisterCrew(crew);
            Queue<CrewActionBase> crewActions = task.CrewActions;
            yield return Handler.Act(crewActions);
            task.OnCompleted();
            DoingTask = null;
            actionCoroutine = null;
            Debug.Log("COMPLETE TASK");
            OnFree();

        }
    }
}