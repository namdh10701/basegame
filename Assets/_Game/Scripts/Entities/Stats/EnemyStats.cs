﻿using System;
using System.Collections.Generic;
using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Entities;
using UnityEngine;

namespace _Game.Scripts
{
    // [CreateAssetMenu(fileName = "EnemyStats", menuName = "Scriptable Objects/Enemy Stats", order = 1)]
    [Serializable]
    public class EnemyStats : Stats, IAliveStats, IFighterStats, IMoverStats
    {
        [field: SerializeField]
        public RangedStat HealthPoint { get; set; } = new(500, 0, 800);

        [field: SerializeField]
        public Stat BlockChance { get; set; } = new();

        [field: SerializeField]
        public Stat AttackDamage { get; set; } = new();

        [field: SerializeField]
        public Stat CriticalChance { get; set; } = new();

        [field: SerializeField]
        public Stat CriticalDamage { get; set; } = new();

        [field: SerializeField]
        public Stat AttackRange { get; set; } = new();

        public Stat AttackAccuracy { get; set; } = new();

        public Stat EvasionChance { get; set; } = new();

        [field: SerializeField]
        public Stat ActionSequenceInterval { get; set; } = new();
        [field: SerializeField]
        public Stat EvadeChance { get; set; } = new();

        [field: SerializeField]
        public Stat Poise { get; set; } = new(1);

        public IFighterStats.AttackTypes AttackType { get; set; } = IFighterStats.AttackTypes.UNIT;
        [field: SerializeField]
        public Stat MoveSpeed { get; set; } = new();
        [field: SerializeField]
        public Stat AnimationTimeScale { get; set; } = new();
        [field: SerializeField]
        public Stat FeverPoint { get; set; } = new();
        public List<Stat> SlowableStats => new List<Stat> { MoveSpeed, AnimationTimeScale};
    }
}