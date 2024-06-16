using _Base.Scripts.RPG;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IEffectGiver))]
public class EffectGiverCollisionListener : MonoBehaviour
{
    public EffectCollisionDetector EffectCollisionDetector;
    public EffectCollisionHandler CollisionHandler = new DefaultEffectCollisionHandler();
    public IEffectGiver Giver;

    private void Awake()
    {
        Giver = GetComponent<IEffectGiver>();
        EffectCollisionDetector.OnEntityCollisionEnter += OnEntityCollisionEnter;
    }

    private void OnDestroy()
    {
        EffectCollisionDetector.OnEntityCollisionEnter -= OnEntityCollisionEnter;
    }

    private void OnEntityCollisionEnter(IEffectTaker effectTakerOnCollision)
    {
        CollisionHandler.Process(Giver, effectTakerOnCollision);
    }
}
