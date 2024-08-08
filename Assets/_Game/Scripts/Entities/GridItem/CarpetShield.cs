using _Base.Scripts.RPG.Stats;
using _Game.Features.Gameplay;
using _Game.Scripts.Entities;
using Online;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public abstract class CarpetShield : MonoBehaviour
    {
        public CarpetShieldData carpets;
        public Stat shieldCooldown = new();
        public RangedStat Shield = new(0, 0);
        float elapsedTime;
        protected abstract int EffectId();
        bool isFirst;
        protected IBuffable attached;
        public virtual void Init(Carpet carpet)
        {
            carpets = carpet.GetShieldData(EffectId());
            shieldCooldown.BaseValue = carpets.cooldown;
            CalculateStats();
            carpet.OnActive += OnCarpetStateChanged;
            IBuffable buffable = GetComponent<IBuffable>();
            attached = buffable;
            Buff(buffable);
        }
        private void Awake()
        {
            Shield.OnValueChanged += Shield_OnValueChanged;
        }

        private void Shield_OnValueChanged(RangedStat obj)
        {
            if (obj.Value < obj.MinValue)
            {
                cooldown = true;
                OnShieldCooldownStart();
            }
        }
        public virtual void OnShieldCooldownStart() { }
        public virtual void OnShieldCooldownStop() { }
        public abstract void Buff(IBuffable buffable);
        public abstract void RemoveBuff(IBuffable buffable);
        protected virtual void OnCarpetStateChanged(Carpet carpet, bool isActive)
        {
            if (isActive)
            {
                cooldown = false;
                carpets = (carpet.GetShieldData(EffectId()));
            }
            else
            {
                carpets = null;
            }
            CalculateStats();
        }

        protected virtual void CalculateStats()
        {
            float shield = 0;
            if (carpets != null)
            {
                shield = carpets.shield;
            }
            Shield.MaxStatValue.BaseValue = shield;
        }
        protected bool cooldown;
        protected virtual void Update()
        {
            if (!cooldown)
            {
                elapsedTime += Time.deltaTime;
                if (elapsedTime > shieldCooldown.Value)
                {
                    Shield.StatValue.BaseValue = Shield.MaxValue;
                    elapsedTime = 0;
                    OnShieldCooldownStop();
                    cooldown = true;
                }

            }
        }

    }
}