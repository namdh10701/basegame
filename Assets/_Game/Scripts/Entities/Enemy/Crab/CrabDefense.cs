using _Base.Scripts.RPG;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Stats;
using _Game.Scripts;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
namespace _Game.Features.Gameplay
{
    public class CrabDefense : MonoBehaviour, IEffectTaker, IStatsBearer
    {
        public EffectHandler effectHandler;
        public EnemyStats enemyStats;
        public Transform Transform => transform;

        public EffectHandler EffectHandler => effectHandler;

        public Stats Stats => enemyStats;

        public Stat StatusResist => null;

        public EffectTakerCollider effectCollider;
        public Crab crab;
        public CrabView crabView;

        public void ApplyStats()
        {
        }
        private void Start()
        {
            effectHandler.EffectTaker = this;
            effectCollider.Taker = this;
            enemyStats.HealthPoint.MaxStatValue.BaseValue = crab._stats.HealthPoint.Value / 3;
            enemyStats.HealthPoint.OnValueChanged += HealthPoint_OnValueChanged;
            crabView.OnDefenseEntered += Active;
            crabView.OnDefenseExit += Deactive;
        }

        private void HealthPoint_OnValueChanged(_Base.Scripts.RPG.Stats.RangedStat obj)
        {
            if (obj.Value <= obj.MinValue)
            {
                if (!isActive)
                    return;
                OnBroken?.Invoke();
                Deactive();
            }
        }


        bool isActive;
        public Action OnBroken;
        public Action OnActive;
        public Action OnDeactive;


        public void Active()
        {
            if (isActive)
                return;
            isActive = true;
            enemyStats.HealthPoint.StatValue.BaseValue = enemyStats.HealthPoint.MaxStatValue.Value;
            OnActive?.Invoke();
            effectCollider.gameObject.SetActive(true);
        }

        public void Deactive()
        {
            OnDeactive?.Invoke();
            isActive = false;
            effectCollider.gameObject.SetActive(false);

        }
    }
}