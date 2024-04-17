using System.Collections.Generic;
using _Base.Scripts.RPG.Effects;

namespace _Base.Scripts.RPGCommon.Entities
{
    public interface IShooter: IFighter
    {
        // public Vector3 AimDirection { get; }
        // public IAttackStrategy AttackStrategy = new List<Effect>();
        public List<Effect> BulletEffects { get; set; }
    }
}