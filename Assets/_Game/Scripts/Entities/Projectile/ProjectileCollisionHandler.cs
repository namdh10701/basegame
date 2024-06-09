using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Entities;
using System.Collections.Generic;
using UnityEngine;
public interface IHandler
{
    public bool IsCompleted { get; }
    public void Process(Projectile p, Entity mainEntity, Entity collidedEntity);
}
public class ProjectileCollisionHandler : DefaultCollisionHandler
{
    Projectile projectile;
    public List<IHandler> Handlers = new List<IHandler>();
    public List<IHandler> LoopHandlers = new List<IHandler>();
    public List<Entity> IgnoreCollideEntities = new List<Entity>();

    public ProjectileCollisionHandler(Projectile projectile)
    {
        this.projectile = projectile;
    }

    public override void Process(Entity mainEntity, Entity collidedEntity)
    {
        if (IgnoreCollideEntities.Contains(collidedEntity))
        {
            return;
        }
        IgnoreCollideEntities.Add(collidedEntity);
        base.Process(mainEntity, collidedEntity);
        foreach (IHandler handler in LoopHandlers.ToArray())
        {
            handler.Process(projectile, mainEntity, collidedEntity);
        }
        foreach (IHandler handler in Handlers.ToArray())
        {
            handler.Process(projectile, mainEntity, collidedEntity);
            if (handler.IsCompleted)
            {
                Handlers.Remove(handler);
            }
        }
        if (Handlers.Count == 0)
        {
            Object.Destroy(mainEntity.gameObject);
        }
    }
}

public class ParticleHandler : IHandler
{
    ParticleSystem onHitParticle;
    public ParticleHandler(ParticleSystem onHitParticle)
    {
        this.onHitParticle = onHitParticle;
    }

    bool IHandler.IsCompleted { get => false; }
    void IHandler.Process(Projectile p, Entity mainEntity, Entity collidedEntity)
    {
        GameObject particle = GameObject.Instantiate(onHitParticle.gameObject, p.transform.position, Quaternion.identity, null);
        particle.SetActive(true);
    }
}

public class PiercingHandler : IHandler
{
    public int piercingCount;
    public int maxPiercing;
    public List<Entity> collided;
    public PiercingHandler(int maxPiercing)
    {
        this.maxPiercing = maxPiercing;
    }

    public bool IsCompleted => piercingCount >= maxPiercing;

    void IHandler.Process(Projectile p, Entity mainEntity, Entity collidedEntity)
    {
        piercingCount++;
    }
}
