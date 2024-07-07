using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Stats;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Base.Scripts.RPG.Entities
{
    public abstract class Entity : MonoBehaviour, IStatsBearer
    {
        public abstract _Game.Scripts.Stats Stats { get; }

        public abstract void ApplyStats();
    }
}