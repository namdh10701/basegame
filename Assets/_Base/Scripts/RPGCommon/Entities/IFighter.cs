using _Base.Scripts.RPG.Attributes;
using _Base.Scripts.RPG.Stats;
using _Game.Scripts.Attributes;

namespace _Base.Scripts.RPGCommon.Entities
{
    public interface IFighter
    {
        public enum AttackTypes
        {
            AREA,
            UNIT
        }
        Stat AttackDamage { get; set; }
        Stat CriticalChance { get; set; }
        Stat CriticalDamage { get; set; }
        
        Stat AttackRange { get; set; }
        
        AttackTypes AttackType { get; set; } // area, unit
    }
}