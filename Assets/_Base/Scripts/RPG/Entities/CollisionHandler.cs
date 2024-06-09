using UnityEngine;

namespace _Base.Scripts.RPG.Entities
{
    public interface ICollisionHandler
    {
        void Process(Entity mainEntity, Entity collidedEntity);
    }

    public abstract class CollisionHandler : ICollisionHandler
    {
        public abstract void Process(Entity mainEntity, Entity collidedEntity);
    }

    public class DefaultCollisionHandler : CollisionHandler
    {
        public override void Process(Entity mainEntity, Entity collidedEntity)
        {
            foreach (var effect in mainEntity.OutgoingEffects)
            {
                if (!effect.CanEffect(collidedEntity))
                {
                    continue;
                }
                collidedEntity.EffectHandler?.Apply(effect);
            }
        }
    }
}