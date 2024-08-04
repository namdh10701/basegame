using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
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
    public Stat StatusResist { get; }
}

public interface IGriditemEffectTaker
{
    public Transform Transform { get; }
    public GridItemEffectHandler EffectHandler { get; }
    public Stat StatusResist { get; }
}

public interface IPhysicsEffectTaker : IEffectTaker
{
    public Rigidbody2D Body { get; }
    public float Poise { get; }
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