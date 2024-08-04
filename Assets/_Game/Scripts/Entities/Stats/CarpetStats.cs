using _Base.Scripts.RPG.Stats;
using _Game.Scripts;
using System;
using UnityEngine;
namespace _Game.Features.Gameplay
{
    public enum CarpetType
    {
        Survival, Defense, Attack, Resource
    }
    [Serializable]
    public class CarpetStats : Stats
    {
        //Survival
        public Stat ShipHpRegen = new();
        public Stat LifeSteal = new();
        public Stat LowerAmmoMana = new();
        public Stat CrewRepairSpeedBoost = new();

        //Defense
        public Stat BlockProjectile = new();
        public Stat ShieldAdjItem = new();
        public Stat ShieldShip = new();
        public Stat BoostDmgWhileShield = new();
        public Stat Cooldown = new();

        //Attack
        public Stat IncreaseDmg = new();
        public Stat IncreaseAtkSpeed = new();
        public Stat AmmoBonus = new();
        public Stat InstanceKillChance = new();
        public Stat IncreaseFeverTime = new();

        public Stat GoldEarning = new();
        public Stat Luck = new();
    }
}