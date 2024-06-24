using _Base.Scripts.RPG.Attributes;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.StateMachine;
using _Game.Scripts.Entities;
using _Game.Scripts.Gameplay.Ship;
using _Game.Scripts.PathFinding;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    public class Crew : Entity, IGridItem, INodeOccupier, IEffectTaker, IStunable
    {
        public CrewActionHandler ActionHandler;
        public CrewMovement CrewMovement;

        public CrewStats stats;
        public override Stats Stats => stats;
        public CrewStatsTemplate _statTemplate;

        [Header("Crew")]
        public Ship Ship;
        public PathfindingController pathfinder;
        public CrewAniamtionHandler Animation;

        [Header("EffectTaker")]
        public EffectHandler effectHandler;
        public Transform Transform => EffectTakerCollider.transform;

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
        public MoveData MoveData;
        public EffectTakerCollider EffectTakerCollider;

        public List<Node> occupiyingNodes = new List<Node>();
        public List<Node> OccupyingNodes { get => occupiyingNodes; set => occupiyingNodes = value; }
        Idle idle;
        Wander wander;

        public Bullet carryingBullet;
        public Bullet CarryingBullet
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
            }
        }
        private void Start()
        {
            EffectTakerCollider.Taker = this;
            Ship = FindAnyObjectByType<Ship>();
            pathfinder = Ship.PathfindingController;
            ActionHandler.OnFree += OnFree;
            idle = new Idle(this);
            wander = new Wander(this, MoveData);

            ActionHandler.Act(idle);
        }

        public bool IsAllowWander = false;
        void OnFree()
        {
            if (crewController == null)
            {
                return;
            }
            if (crewController.HasPendingJob)
            {
                crewController.RegisterForNewJob(this);
            }
            else
            {
                if (!IsAllowWander)
                {
                    ActionHandler.Act(new Idle(this));
                }
                else
                {
                    float rand = UnityEngine.Random.Range(0f, 1f);
                    if (rand < 0.35f)
                    {
                        ActionHandler.Act(new Idle(this));
                    }
                    else
                    {
                        ActionHandler.Act(new Wander(this, MoveData));
                    }
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

        public void OnStun(float duration)
        {
            ActionHandler.Pause();
            Animation.PlayStun();
            Debug.LogError("Stun");
            body.velocity = Vector2.zero;
            CancelInvoke();
            Invoke("OnAfterStun", duration);
        }

        public void OnAfterStun()
        {
            ActionHandler.Resume();
        }

        public void Carry(Bullet bullet)
        {
            Animation.PlayCarry();
            CarryingBullet = bullet;
            carryObject.gameObject.SetActive(true);
            carryObject.sprite = bullet.Def.Image;
        }

        public void StopCarry()
        {
            Animation.PlayIdle();
            CarryingBullet = null;
            carryObject.gameObject.SetActive(false);
        }

        public void Deactivate()
        {

        }

        public void OnFixed()
        {

        }
    }
}


