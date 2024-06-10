using _Base.Scripts.RPG.Entities;
using _Game.Scripts.GD;
using JetBrains.Annotations;
using UnityEngine;

namespace _Game.Scripts.Gameplay.Ship
{
    public class Ship : Entity
    {
        [Space]
        [Header("Ship")]
        public string Id;
        public ShipStats stats;
        public ShipSetup ShipSetup;
        public override Stats Stats => stats;

        private void Start()
        {
            LoadShipStats();
            LoadModifiers();
            ShipSetup.LoadShipItems();
        }

        void LoadShipStats()
        {
            if (GDConfigLoader.Instance != null)
            {
                if (GDConfigLoader.Instance.Ships.TryGetValue(Id, out ShipConfig shipConfig))
                {
                    ApplyConfig(shipConfig);
                }
            }
            else
            {
                stats = ResourceLoader.LoadShipTemplateConfig(Id).Data;
            }

        }

        void ApplyConfig(ShipConfig shipConfig)
        {

        }

        void LoadModifiers()
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
