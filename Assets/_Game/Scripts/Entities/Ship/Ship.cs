using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Game.Scripts.GD;
using JetBrains.Annotations;
using UnityEngine;

namespace _Game.Scripts.Gameplay.Ship
{
    public class Ship : Entity, IEffectTaker
    {
        [Space]
        [Header("Ship")]
        public string Id;
        public ShipStats stats;
        public ShipSetup ShipSetup;
        public override Stats Stats => stats;
        public ShipStatsTemplate _statTemplate;
        public EffectHandler effectHandler;

        public Transform Transform => transform;

        public EffectHandler EffectHandler => effectHandler;
        public PathfindingController PathfindingController;
        public EffectTakerCollider EffectCollider;
        public ShipSpeed ShipSpeed;
        public CrewController CrewController;

        protected override void Awake()
        {
            base.Awake();
            EffectCollider.Taker = this;
        }
        private void Start()
        {
            ShipSetup.LoadShipItems();
        }
        protected override void LoadStats()
        {
            if (GDConfigLoader.Instance != null)
            {
                if (GDConfigLoader.Instance.Ships.TryGetValue(Id, out ShipConfig shipConfig))
                {
                    shipConfig.ApplyGDConfig(stats);
                }
            }
            else
            {
                _statTemplate.ApplyConfig(stats);
            }
        }

        protected override void LoadModifiers()
        {

        }

        protected override void ApplyStats()
        {

        }


        private void Update()
        {
            RegenMP();
            RegenHP();
        }

        void RegenMP()
        {
            if (!stats.ManaPoint.IsFull)
            {
                stats.ManaPoint.StatValue.BaseValue += (stats.ManaRegenerationRate.Value * Time.deltaTime);
            }
        }

        void RegenHP()
        {
            if (!stats.HealthPoint.IsFull)
            {
                stats.HealthPoint.StatValue.BaseValue += (stats.HealthRegenerationRate.Value * Time.deltaTime);
            }
        }

    }
}
