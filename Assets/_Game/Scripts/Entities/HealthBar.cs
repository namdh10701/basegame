using System;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts.Attributes;
using UnityEngine;

namespace _Game.Scripts.Entities
{
    public class HealthBar: MonoBehaviour
    {
        public Entity entity;
        public RectTransform background;
        public RectTransform fill;

        private IAlive _alive;
        private void Awake()
        {
            _alive = entity.Stats as IAlive;
        }

        private void Update()
        {
            var percentage = _alive.HealthPoint / _alive.MaxHealthPoint.Value;
            var fillSize = background.sizeDelta.x * percentage;
            fill.sizeDelta = new Vector2(fillSize, fill.sizeDelta.y);
        }
    }
}