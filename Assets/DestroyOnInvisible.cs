using _Base.Scripts.RPGCommon.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnInvisible : MonoBehaviour
{
    Projectile projectile;
    private void OnBecameInvisible()
    {
        projectile = transform.parent.GetComponent<Projectile>();
        ProjectileCollisionHandler handler = (ProjectileCollisionHandler)projectile.CollisionHandler;
        handler.FinalAct();
    }
}
