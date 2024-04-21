using _Base.Scripts.RPG.Stats;
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
                GlobalContext.PlayerContext.ManaPoint = new RangedValue(stat.Value, stat.MinValue, stat.MaxValue);
            };
            
            stats.HealthPoint.OnValueChanged += (stat) =>
            {
                GlobalContext.PlayerContext.HealthPoint = new RangedValue(stat.Value, stat.MinValue, stat.MaxValue);;
            };
        }
    }
}
