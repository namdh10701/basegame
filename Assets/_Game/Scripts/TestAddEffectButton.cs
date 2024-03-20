    using System;
    using _Base.Scripts.EventSystem;
    using _Base.Scripts.RPG;
    using _Game.Scripts.Attributes;
    using _Game.Scripts.Effects;
    using _Game.Scripts.Entities;
    using Unity.VisualScripting;
    using UnityEngine;

    namespace _Game.Scripts
    {
        public class TestAddEffectButton: MonoBehaviour
        {
            public Cannon cannon;
            public void DecreaseHealth()
            {
                var eff = cannon.effectHolder.AddComponent<DecreaseHealthPointEffect>();
                eff.Amount = 10;
            }
            
            public void IncreaseHealth()
            {
                var eff = cannon.effectHolder.AddComponent<IncreaseHealthPointEffect>();
                eff.Amount = 10;
            }
            
            public void DrainHealth()
            {
                var eff = cannon.effectHolder.AddComponent<DrainHealthPointEffect>();
                eff.Amount = 10;
                eff.Duration = 10;
                eff.Interval = 1;
            }
        }
    }
