using _Base.Scripts.RPG.Attributes;
using _Base.Scripts.RPG.Stats;
using _Game.Scripts.Attributes;

namespace _Base.Scripts.RPGCommon.Entities
{
    public interface IAlive
    {
        float HealthPoint { get; set; }
        
        Stat MaxHealthPoint { get; set; }
    }
}