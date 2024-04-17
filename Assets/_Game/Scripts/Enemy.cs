using _Base.Scripts.RPG.Entities;
using _Game.Scripts.Attributes;
    using UnityEngine;

namespace _Game.Scripts
    {
        public class Enemy: Entity//, IAlive
        {
            [SerializeField]
            private CannonStats stats = new ();
            public override Stats Stats => stats;

            public HealthPoint HealthPoint { get; set; }
        }
    }
