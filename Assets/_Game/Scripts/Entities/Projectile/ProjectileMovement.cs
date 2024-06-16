
using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts;
using UnityEngine;

public abstract class ProjectileMovement
{
    protected Rigidbody2D body;
    protected Stat MoveSpeed;
    public ProjectileMovement(Projectile projectile)
    {
        this.body = projectile.body;
        MoveSpeed = ((ProjectileStats)projectile.Stats).Speed;
    }
    public abstract void Move();

}

public class StraightMove : ProjectileMovement
{
    Vector3 direction;
    public StraightMove(Projectile projectile) : base(projectile)
    {
        direction = projectile.transform.up;
    }
    public override void Move()
    {
        body.velocity = direction * MoveSpeed.Value;
    }
}
public class HomingMove : ProjectileMovement
{
    public Transform target;
    public float force = 5;

    public HomingMove(Projectile projectile, Transform target) : base(projectile)
    {
        this.target = target;
    }

    public override void Move()
    {
        if (target != null)
        {
            Vector2 Direction = (Vector2)target.position - body.position;
            body.velocity = Direction.normalized * MoveSpeed.Value;
            body.transform.up = Direction.normalized;
        }
    }
}

