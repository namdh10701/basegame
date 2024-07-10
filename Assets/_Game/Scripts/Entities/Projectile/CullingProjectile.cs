using _Game.Scripts.Entities;
using UnityEngine;

public class CullinglProjectile : CannonProjectile
{
    protected override void Awake()
    {
        base.Awake();
        ProjectileCollisionHandler projectileCollisionHandler = (ProjectileCollisionHandler)collisionListener.CollisionHandler;
    }

    public override void ApplyStats()
    {
        base.ApplyStats();

        GameObject ks = new GameObject("Skil Shot");
        KillShot killShot = ks.AddComponent<KillShot>();
        outGoingEffects.Add(killShot);
    }
}
