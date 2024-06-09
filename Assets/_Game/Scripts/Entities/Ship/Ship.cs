using _Base.Scripts.RPG.Entities;
using _Game.Scripts.GD;
using UnityEngine;

namespace _Game.Scripts.Gameplay.Ship
{
    public class Ship : Entity
    {
        public string Id;
        [SerializeField] ShipStats stats;
        public override Stats Stats => stats;
        public ShipSetup ShipSetup;


        protected override void Awake()
        {
            base.Awake();
        }
        private void Start()
        {
            LoadShipStats();
            ShipSetup.LoadShipItems();
        }

        void LoadShipStats()
        {
            if (GDConfigLoader.Instance.Ships.TryGetValue(Id, out ShipConfig shipConfig))
            {

            }
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
