using _Base.Scripts.RPG.Attributes;
using _Base.Scripts.RPG.Stats;
using _Game.Scripts.Attributes;

namespace _Base.Scripts.RPGCommon.Entities
{
    public interface IAliveStats
    {
        RangedStat HealthPoint { get; set; }
        
        Stat BlockChance { get; set; }
    }
}