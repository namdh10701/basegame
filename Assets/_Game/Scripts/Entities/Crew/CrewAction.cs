using _Game.Scripts;
using _Game.Scripts.Entities;
using _Game.Scripts.PathFinding;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class CrewAction : MonoBehaviour
    {
        [SerializeField] Crew crew;
        CrewController crewController;
        [SerializeField] CrewActionHandler Handler;
        Idle idle;
        Wander wander;
        bool isActive;
        public SpriteRenderer carryObject;
        private Ammo carryingBullet;

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
        public CrewActionBase CurrentAction => Handler.CurrentAction;

        private void Start()
        {
            MoveData moveData = FindAnyObjectByType<MoveData>();
            crewController = FindAnyObjectByType<CrewController>();
            idle = new Idle(crew);
            wander = new Wander(crew, moveData);
            Handler.Act(idle);
            Handler.OnFree += OnFree;
        }

        void OnFree()
        {
            if (!isActive || crewController == null)
            {
                Handler.Act(new Idle(crew));
                return;
            }
            if (crewController.HasPendingJob)
            {
                if (crewController.RegisterForNewJob(crew))
                {

                }
                else
                {
                    float rand = Random.Range(0f, 1f);
                    if (rand < 0.35f)
                    {
                        Handler.Act(new Idle(crew));
                    }
                    else
                    {
                        MoveData moveData = FindAnyObjectByType<MoveData>();
                        Handler.Act(new Wander(crew, moveData));
                    }
                }
            }
            else
            {
                float rand = Random.Range(0f, 1f);
                if (rand < 0.35f)
                {
                    Handler.Act(new Idle(crew));
                }
                else
                {
                    MoveData moveData = FindAnyObjectByType<MoveData>();
                    Handler.Act(new Wander(crew, moveData));
                }
            }
        }

        public void DoJob(CrewJobAction crewJobAction)
        {//CHECK HERE
            Handler.Act(crewJobAction);
        }
        public void Pause()
        {
            Handler.PauseCurrentAction();
        }
        public void Resume()
        {
            Handler.ResumeCurrentAction();
        }
        public void Activate()
        {
            isActive = true;
        }
        public void Deactivate()
        {
            isActive = false;
        }
    }
}