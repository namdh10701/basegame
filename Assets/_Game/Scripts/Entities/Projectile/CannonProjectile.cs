using _Base.Scripts.RPG.Attributes;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPGCommon.Entities;

namespace _Game.Scripts.Entities
{
    public class CannonProjectile : Projectile
    {
        public override Stats Stats { get; }
        protected override void Awake()
        {
            base.Awake();
            OutgoingEffects.Add(new DecreaseHealthEffect(3));
        }
    }
}