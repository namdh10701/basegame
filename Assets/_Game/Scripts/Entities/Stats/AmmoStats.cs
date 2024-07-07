using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts;
using System;
using UnityEngine;
[Serializable]
public class AmmoStats : Stats, IAliveStats
{
    [field: SerializeField]
    public RangedStat HealthPoint { get; set; }
    [field: SerializeField]
    public Stat EnergyCost;
    [field: SerializeField]
    public Stat MagazineSize;
    [field: SerializeField]
    public Stat ProjectileCount;
}
