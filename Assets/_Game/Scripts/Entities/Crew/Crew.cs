using _Base.Scripts.RPG.Attributes;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.StateMachine;
using _Game.Scripts.Entities;
using _Game.Scripts.Gameplay.Ship;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    public class Crew : Entity, IGridItem, IEffectTaker
    {
        public CrewActionHandler ActionHandler;
        public CrewStats stats;
        public override Stats Stats => stats;
        public CrewStatsTemplate _statTemplate;

        [Header("Crew")]
        public Ship Ship;
        public PathfindingController pathfinder;
        public CrewAniamtionHandler Animation;

        [Header("EffectTaker")]
        public EffectHandler effectHandler;
        public Transform Transform => transform;

        public EffectHandler EffectHandler => effectHandler;


        [Header("GridItem")]
        public GridItemDef def;
        public Transform behaviour;
        [field: SerializeField]
        public List<Cell> OccupyCells { get; set; }
        public GridItemDef Def { get => def; set => def = value; }
        public Transform Behaviour { get => behaviour; }
        public string GridId { get; set; }

        public SpriteRenderer carryObject;
        public CrewController crewController;
        public WanderData WanderData;
        Idle idle;
        Wander wander;
        private void Start()
        {
            Ship = FindAnyObjectByType<Ship>();
            pathfinder = Ship.PathfindingController;
            ActionHandler.OnFree += OnFree;
            idle = new Idle(this);
            wander = new Wander(this, WanderData);


            ActionHandler.Act(idle);
        }

        void OnFree()
        {
            if (crewController.HasPendingJob)
            {
                crewController.RegisterForNewJob(this);
            }
            else
            {
                float rand = UnityEngine.Random.Range(0f, 1f);
                if (rand < 0.5f)
                {
                    ActionHandler.Act(idle);
                }
                else
                {
                    ActionHandler.Act(wander);
                }
            }
        }

        protected override void ApplyStats()
        {
        }

        protected override void LoadModifiers()
        {

        }

        protected override void LoadStats()
        {
            _statTemplate.ApplyConfig(stats);
        }
    }
}


