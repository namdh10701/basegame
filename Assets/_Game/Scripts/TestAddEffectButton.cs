    using System;
    using _Base.Scripts.EventSystem;
    using _Base.Scripts.RPG;
    using _Base.Scripts.RPG.Effects;
    using _Base.Scripts.RPG.Entities;
    using _Base.Scripts.RPGCommon.Entities;
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
                // var alive = cannon.GetComponent<Cannon>().Stats as IAlive;
                // if (alive != null) alive.HealthPoint -= 10;
                
                var cann = cannon.GetComponent<Cannon>();
                cann.EffectHandler.Apply(new DecreaseHealthEffect(10));
            }
            
            public void IncreaseHealth()
            {
                // var alive = cannon.GetComponent<Cannon>().Stats as IAlive;
                // if (alive != null) alive.HealthPoint += 10;
                //
                var cann = cannon.GetComponent<Cannon>();
                cann.EffectHandler.Apply(new IncreaseHealthEffect(10));
            }
            
            public void DrainHealth()
            {
                var cann = cannon.GetComponent<Cannon>();
                cann.EffectHandler.Apply(new DrainHealthEffect(10, 1, 10));
                
                // cann.eff
            }
            
            public void AddTempHealth()
            {
                // var eff = cannon.effectHolder.AddComponent<TempIncreaseHealthPointEffect>();
                // eff.Amount = 500;
                // eff.Duration = 5;
            }
        }
    }
