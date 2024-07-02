using _Base.Scripts.RPG.Stats;

namespace _Base.Scripts.RPGCommon.Entities
{
    public interface IAliveStats
    {
        RangedStat HealthPoint { get; set; }
    }
}