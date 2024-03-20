    using System;
    using _Base.Scripts.EventSystem;
    using _Base.Scripts.RPG;
    using _Game.Scripts.Attributes;

    namespace _Game.Scripts
    {
        public class Enemy: Entity, ILivingEntity
        {
            private void Awake()
            {
                // Attributes.Add(new HealthPoint());
                // Attributes.Add(new ManaPoint());
            }

            public HealthPoint HealthPoint { get; set; }
        }
    }
