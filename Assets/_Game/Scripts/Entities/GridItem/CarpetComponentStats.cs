using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    [Serializable]
    public class CarpetComponentStats : Stats, IAliveStats
    {
        [field: SerializeField]
        public RangedStat HealthPoint { get; set; } = new(1, 0, 1);

    }
}