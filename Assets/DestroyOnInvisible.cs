using _Base.Scripts.RPGCommon.Entities;
using UnityEngine;
namespace _Game.Features.Gameplay
{
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
}