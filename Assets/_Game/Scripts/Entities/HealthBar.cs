using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Entities;
using UnityEngine;

namespace _Game.Scripts.Entities
{
    public class HealthBar: MonoBehaviour
    {
        public Entity entity;
        public RectTransform background;
        public RectTransform fill;

        private IAliveStats _aliveStats;
        private void Awake()
        {
            _aliveStats = entity.Stats as IAliveStats;
        }

        private void Update()
        {
            if (_aliveStats == null)
            {
                return;
            }
            var percentage = _aliveStats.HealthPoint.PercentageValue;
            var fillSize = background.sizeDelta.x * percentage;
            fill.sizeDelta = new Vector2(fillSize, fill.sizeDelta.y);
        }
    }
}