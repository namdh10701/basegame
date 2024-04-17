using _Game.Scripts.GameContext;
using UnityEngine;

namespace _Game.Scripts.Gameplay.Ship
{
    public class ShipDataBinding : MonoBehaviour
    {
        public Ship ship;
        private void Start()
        {
        }

        protected void Awake()
        {
            var stats = ship.Stats as ShipStats;
            stats.ManaPoint.OnValueChanged += (stat) =>
            {
                GlobalContext.PlayerContext.ManaPoint = stat;
            };
            
            stats.HealthPoint.OnValueChanged += (stat) =>
            {
                GlobalContext.PlayerContext.HealthPoint = stat;
            };
        }
    }
}
