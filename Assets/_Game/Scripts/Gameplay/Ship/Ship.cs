using _Base.Scripts.RPG.Entities;
using _Base.Scripts.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.Gameplay.Ship
{
    public class Ship : Entity//SingletonMonoBehaviour<Ship>
    {
        public static Ship Instance;
        // public ShipMana ShipMana;
        [SerializeField]
        private ShipStats stats = new ();
        public override Stats Stats => stats;

        protected override void Awake()
        {
            base.Awake();
            Instance = this;
        }

        private void Start()
        {
        }

        private void Update()
        {
            if (!stats.ManaPoint.IsFull)
            {
                stats.ManaPoint.Value += (stats.ManaRegenerationRate.Value * Time.deltaTime);
            }

            if (!stats.HealthPoint.IsFull)
            {
                stats.HealthPoint.Value += (stats.HealthRegenerationRate.Value * Time.deltaTime);
            }
        }

    }
}
