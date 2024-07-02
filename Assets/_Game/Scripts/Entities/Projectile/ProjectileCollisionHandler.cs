using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts;
using _Game.Scripts.Entities;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public interface IHandler
{
    public bool IsCompleted { get; }
    public void Process(Projectile p, IEffectGiver mainEntity, IEffectTaker collidedEntity);
}
public class ProjectileCollisionHandler : DefaultEffectCollisionHandler
{
    Projectile projectile;
    public List<IHandler> Handlers = new List<IHandler>();
    public List<IHandler> LoopHandlers = new List<IHandler>();
    public List<IEffectTaker> IgnoreCollideEntities = new List<IEffectTaker>();

    public ProjectileCollisionHandler(Projectile projectile)
    {
        this.projectile = projectile;
    }

    public override void Process(IEffectGiver giver, IEffectTaker taker)
    {
        if (taker is Enemy enemy)
        {
            float evadeChance = ((EnemyStats)enemy.Stats).EvadeChance.Value;
            if (evadeChance > 0)
            {
                if (Random.Range(0, 1f) < evadeChance)
                {
                    return;
                }
            }
        }

        if (IgnoreCollideEntities.Contains(taker))
        {
            return;
        }
        IgnoreCollideEntities.Add(taker);

        foreach (IHandler handler in LoopHandlers.ToArray())
        {
            handler.Process(projectile, giver, taker);
        }
        foreach (IHandler handler in Handlers.ToArray())
        {
            handler.Process(projectile, giver, taker);
            if (handler.IsCompleted)
            {
                Debug.Log("completed");
                Handlers.Remove(handler);
            }
        }
        if (Handlers.Count == 0)
        {
            FinalAct();
        }
        base.Process(giver, taker);
    }

    public void FinalAct()
    {
        if (projectile.trail != null)
        {
            projectile.trail.parent = null;
            projectile.trail.AddComponent<DestroyAfterEnabled>();
        }
        Object.Destroy(projectile.gameObject);
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
    void IHandler.Process(Projectile p, IEffectGiver mainEntity, IEffectTaker collidedEntity)
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

    public bool IsCompleted => piercingCount >= maxPiercing + 1;

    void IHandler.Process(Projectile p, IEffectGiver mainEntity, IEffectTaker collidedEntity)
    {
        piercingCount++;
    }
}
