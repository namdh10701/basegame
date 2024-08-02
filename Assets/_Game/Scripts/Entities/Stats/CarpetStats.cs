using _Base.Scripts.RPG.Stats;
using _Game.Scripts;
namespace _Game.Features.Gameplay
{
    public class CarpetStats : Stats
    {
        public Stat Hp = new();
        public Stat Atk = new();
        public Stat CritChance = new();
        public Stat CritDmg = new();
        public Stat MaxMana = new();
        public Stat ManaRegen = new();
        public Stat GoldEarning = new();
        public Stat Luck = new();
    }
}