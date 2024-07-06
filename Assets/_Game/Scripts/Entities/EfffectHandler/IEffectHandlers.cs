using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Game.Scripts;
using System.Collections.Generic;
using UnityEngine;

public interface IEffectGiver
{
    public Transform Transform { get; }
    public List<Effect> OutGoingEffects { get; set; }
}

public interface IEffectTaker
{
    public Transform Transform { get; }
    public EffectHandler EffectHandler { get; }

    public Stats Stats { get; }
}

public interface IPhysicsEffectTaker : IEffectTaker
{
    public Rigidbody2D Body { get; }
    public float Poise { get; }
}

public interface IEffectHandleOnCollision
{

}


public interface IEffectTakerCollider
{
    IEffectTaker Taker { get; }
}

public interface IEffectGiverCollider
{
    EffectCollisionHandler CollisionHandler { get; }
    IEffectGiver Giver { get; }
}