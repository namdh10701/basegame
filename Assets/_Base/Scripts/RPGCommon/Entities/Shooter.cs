using System.Collections.Generic;
using _Base.Scripts.RPG.Behaviours.AttackTarget;
using _Base.Scripts.RPG.Effects;
using UnityEngine;

namespace _Base.Scripts.RPGCommon.Entities
{
    public abstract class Shooter: MonoBehaviour, IShooter
    {
        // public Vector3 AimDirection { get; }
        // public IAttackStrategy AttackStrategy = new List<Effect>();
        public List<Effect> BulletEffects { get; set; }
        public abstract IFighterStats FighterStats { get; set; }
        public abstract AttackStrategy AttackStrategy { get; set; }
    }
}