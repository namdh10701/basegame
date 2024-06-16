using _Game.Scripts;
using UnityEngine;

namespace _Base.Scripts.RPG.Entities
{
    /*    public interface ICollisionHandler
        {
            void Process(Entity mainEntity, Entity collidedEntity);
        }*/

    /*    public abstract class EffectCollider : ICollisionHandler
        {
            public abstract void Process(Entity mainEntity, Entity collidedEntity);
        }*/
    public abstract class EffectCollisionHandler
    {
        public virtual void Process(IEffectGiver giver, IEffectTaker taker)
        {
            foreach (var effect in giver.OutGoingEffects)
            {
                if (!effect.CanEffect(taker))
                {
                    continue;
                }
                taker.EffectHandler?.Apply(effect);
            }
        }
    }
    public class DefaultEffectCollisionHandler : EffectCollisionHandler
    {

    }
}