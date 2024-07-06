using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Game.Scripts.Entities;
using _Game.Scripts.Gameplay.Ship;
using _Game.Scripts.GD;
using _Game.Scripts.PathFinding;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    public class Crew : Entity, IGridItem, INodeOccupier, IEffectTaker, IStunable
    {
        [Header("Crew")]

        public Ship Ship;
        public CrewAniamtionHandler Animation;
        public CrewMovement CrewMovement;
        public CrewAction CrewAction;
        public CrewStats stats;
        public CrewStatsTemplate _statTemplate;

        [Header("GridItem")]
        [SerializeField] GridItemDef def;
        [SerializeField] Transform behaviour;

        [Header("EffectTaker")]
        public EffectTakerCollider EffectTakerCollider;
        [SerializeField] EffectHandler effectHandler;
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
        public bool IsBroken { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public bool IsAbleToTakeHit { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

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
            if (GDConfigLoader.Instance == null)
            {
                _statTemplate.ApplyConfig(stats);
            }
            else
            {
                if (GDConfigLoader.Instance.Crews.TryGetValue(Id, out CrewConfig enemyConfig))
                {
                    enemyConfig.ApplyGDConfig(stats);
                }
                else
                {
                    _statTemplate.ApplyConfig(stats);
                }
            }
        }

        public void OnStun(float duration)
        {
            Debug.Log("Stun " + name);
            CrewAction.Pause();
            Animation.PlayStun();
            body.velocity = Vector2.zero;
            CancelInvoke();
            Invoke("OnAfterStun", duration);
        }

        public void OnAfterStun()
        {
            CrewAction.Resume();
            Animation.StopStun();
        }

        public void Carry(Ammo bullet)
        {
            Animation.PlayCarry();
            CrewAction.CarryingBullet = bullet;
        }

        public void StopCarry()
        {
            Animation.StopCarry();
        }

        public void Dropdown()
        {
            Animation.PlayDropDown();
        }


        public void Deactivate()
        {

        }

        public void OnFixed()
        {

        }
    }
}


