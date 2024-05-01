using _Base.Scripts.RPG.Entities;
using UnityEngine;

namespace _Game.Scripts.Gameplay.Ship
{
    public class Ship : Entity
    {
        public static Ship Instance;
        [SerializeField] ShipStats stats;
        public override Stats Stats => stats;
        public ShipSetup ShipSetup;

        protected override void Awake()
        {
            base.Awake();
            Instance = this;
        }
        private void Start()
        {
            ShipSetup.LoadShipItems();
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
