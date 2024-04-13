    using System;
    using _Base.Scripts.EventSystem;
    using _Base.Scripts.RPG;
    using _Base.Scripts.RPG.Attributes;
    using _Base.Scripts.RPG.Entities;
    using _Game.Scripts.Attributes;

    namespace _Game.Scripts
    {
        public class Player: Entity
        {
            public override Stats Stats { get; }
        }
    }
