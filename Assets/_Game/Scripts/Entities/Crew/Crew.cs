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
        [Header("Crew")]
        public CrewAniamtionHandler             Animation;
        public CrewMovement                     CrewMovement;
        public CrewAction                       CrewAction;
        public CrewStats                        stats;
        public CrewStatsTemplate                _statTemplate;

        [Header("GridItem")]
        [SerializeField] GridItemDef            def;
        [SerializeField] Transform              behaviour;

        [Header("EffectTaker")]
        public EffectTakerCollider              EffectTakerCollider;
        [SerializeField] EffectHandler          effectHandler;
        public override Stats Stats => stats;
        public EffectHandler EffectHandler => effectHandler;
        public Transform Transform => EffectTakerCollider.transform;
        [field: SerializeField]
        public List<Cell> OccupyCells { get; set; }
        public GridItemDef Def { get => def; set => def = value; }
        public Transform Behaviour { get => behaviour; }
        public string GridId { get; set; }
        public List<Node> occupiyingNodes = new List<Node>();
        public List<Node> OccupyingNodes { get => occupiyingNodes; set => occupiyingNodes = value; }

        private void Start()
        {
            EffectTakerCollider.Taker = this;
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
            CrewAction.Pause();
            Animation.PlayStun();
            body.velocity = Vector2.zero;
            CancelInvoke();
            Invoke("OnAfterStun", duration);
        }

        public void OnAfterStun()
        {
            CrewAction.Resume();
        }

        public void Carry(Bullet bullet)
        {
            Animation.PlayCarry();
            CrewAction.CarryingBullet = bullet;
        }

        public void StopCarry()
        {
            Animation.PlayIdle();
            CrewAction.CarryingBullet = null;
        }

        public void Deactivate()
        {

        }

        public void OnFixed()
        {

        }
    }
}

