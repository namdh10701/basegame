using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
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