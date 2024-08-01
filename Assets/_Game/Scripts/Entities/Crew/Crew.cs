using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using _Game.Scripts;
using _Game.Scripts.Entities;
using _Game.Scripts.GD;
using _Game.Scripts.GD.DataManager;
using _Game.Scripts.PathFinding;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public enum CrewState
    {
        Idle, Moving, Carrying_Moving, Carrying_Idle, Reparing, Stun
    }

    public class Crew : Entity, IGDConfigStatsTarget, IStatsBearer, INodeOccupier, IEffectTaker, IStunable
    {
        #region
        private CrewStatsConfigLoader _configLoader;

        public CrewStatsConfigLoader ConfigLoader
        {
            get
            {
                if (_configLoader == null)
                {
                    _configLoader = new CrewStatsConfigLoader();
                }

                return _configLoader;
            }
        }
        #endregion
        public CrewState state;
        public CrewState State
        {
            get => state; set
            {
                CrewState lastState = state;

                state = value;
                if (state != lastState)
                {
                    OnStateChanged.Invoke(value);
                }
            }
        }
        public Action<CrewState> OnStateChanged;

        [Header("GD Config Stats Target")]
        [SerializeField] private string id;
        [SerializeField] private GDConfig gdConfig;
        [SerializeField] private StatsTemplate statsTemplate;

        [Header("Crew")]
        public Ship Ship;
        public Rigidbody2D body;
        public CrewAniamtionHandler Animation;


        public CrewMovement CrewMovement;
        public CrewAction CrewAction;
        public CrewStats stats;

        [Header("EffectTaker")]
        public EffectTakerCollider EffectTakerCollider;
        [SerializeField] EffectHandler effectHandler;
        public EffectHandler EffectHandler => effectHandler;
        public Transform Transform => EffectTakerCollider.transform;
        [field: SerializeField]
        public List<Cell> OccupyCells { get; set; }
        public string GridId { get; set; }
        public List<Node> occupiyingNodes = new List<Node>();
        public List<Node> OccupyingNodes { get => occupiyingNodes; set => occupiyingNodes = value; }
        public string Id { get => id; set => id = value; }
        public GDConfig GDConfig => gdConfig;

        public StatsTemplate StatsTemplate => statsTemplate;

        public override Stats Stats => stats;

        public Stat StatusResist => stats.StatusReduce;

        private void Start()
        {
            var conf = GameData.CrewTable.FindById(id);
            ConfigLoader.LoadConfig(stats, conf);
            ApplyStats();


            EffectHandler.EffectTaker = this;
            EffectTakerCollider.Taker = this;
        }
        public void OnStun()
        {
            CrewAction.Pause();
            State = CrewState.Stun;
            body.velocity = Vector2.zero;
        }

        public void OnAfterStun()
        {
            State = CrewState.Idle;
            CrewAction.Resume();
        }

        public override void ApplyStats()
        {
        }
    }
}


