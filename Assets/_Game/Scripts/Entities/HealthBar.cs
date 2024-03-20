using _Game.Scripts.Attributes;
using UnityEngine;

namespace _Game.Scripts.Entities
{
    public class HealthBar: MonoBehaviour
    {
        public HealthPoint healthPoint;
        public int capacity;
        public RectTransform background;
        public RectTransform fill;

        private void Update()
        {
            var percentage = 1f * healthPoint.Value / capacity;
            var fillSize = background.sizeDelta.x * percentage;
            fill.sizeDelta = new Vector2(fillSize, fill.sizeDelta.y);
        }
    }
}