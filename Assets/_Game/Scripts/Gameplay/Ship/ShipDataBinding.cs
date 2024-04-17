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
            ship.Stats.ManaPoint.OnValueChanged += (stat) =>
            {
                GlobalContext.PlayerContext.ManaPoint = stat;
            };
            
            ship.Stats.HealthPoint.OnValueChanged += (stat) =>
            {
                GlobalContext.PlayerContext.HealthPoint = stat;
            };
        }
    }
}
