using _Base.Scripts.Utils;
using UnityEngine;

namespace _Game.Scripts.Gameplay.Ship
{
    public class Ship : SingletonMonoBehaviour<Ship>
    {
        // public ShipMana ShipMana;
        public ShipStats Stats = new();
        private void Start()
        {
        }

        private void Update()
        {
            if (!Stats.ManaPoint.IsFull)
            {
                Stats.ManaPoint.Value += (Stats.ManaRegenerationRate.Value * Time.deltaTime);
            }

            if (!Stats.HealthPoint.IsFull)
            {
                Stats.HealthPoint.Value += (Stats.HealthRegenerationRate.Value * Time.deltaTime);
            }
        }
    }
}
