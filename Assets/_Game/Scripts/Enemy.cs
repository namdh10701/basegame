    using System;
    using _Base.Scripts.EventSystem;
    using _Base.Scripts.RPG;
    using _Base.Scripts.RPG.Attributes;
    using _Base.Scripts.RPG.Entities;
    using _Base.Scripts.RPGCommon.Entities;
    using _Game.Scripts.Attributes;
    using UnityEngine;

    namespace _Game.Scripts
    {
        public class Enemy: Entity//, IAlive
        {
            [SerializeField]
            private CannonStats _stats = new CannonStats();
            public override Stats Stats => _stats;

            public HealthPoint HealthPoint { get; set; }
        }
    }
