using _Base.Scripts.RPG.Behaviours.AttackTarget;

namespace _Base.Scripts.RPGCommon.Entities
{
    public interface IFighter
    {
        IFighterStats FighterStats { get; set; }
        
        public AttackStrategy AttackStrategy { get; set; }
    }
}