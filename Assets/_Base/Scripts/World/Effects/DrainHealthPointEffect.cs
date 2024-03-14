using _Base.Scripts.World.Attributes;
using UnityEngine;

namespace _Base.Scripts.World.Effects
{
    public class DrainHealthPointEffect: PeriodicEffect
    {
        [field:SerializeField]
        public int Amount { get; set; }
        
        private HealthPoint _hp;

        private void Awake()
        {
            var entity = GetComponent<Entity>();

            if (entity == null)
            {
                return;
            }
            _hp = entity.Attributes.Find(v => v is HealthPoint) as HealthPoint;
        }

        protected override void Apply()
        {
            _hp.Value -= Amount;
        }
    }
}