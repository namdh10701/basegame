using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Game.Features.Gameplay;
using _Game.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserGuide : MonoBehaviour, IEffectGiver
{
    public EffectGiverCollisionListener collisionListener;

    public EffectCollisionHandler CollisionHandler;

    public Transform Transform => transform;
    public List<Effect> outGoingEffects;
    public List<Effect> OutGoingEffects { get => outGoingEffects; set => outGoingEffects = value; }

    bool isLeft;
    internal void Initialize(Cell startCell, bool isLeft)
    {
        this.isLeft = isLeft;
        CollisionHandler = new LaserGuideCollider(this, startCell);
        collisionListener.CollisionHandler = CollisionHandler;
    }

    public class LaserGuideCollider : DefaultEffectCollisionHandler
    {
        LaserGuide laserGuide;
        Cell startCell;

        bool isActive;
        public LaserGuideCollider(LaserGuide laserGuide, Cell startCell)
        {
            this.laserGuide = laserGuide;
            this.startCell = startCell;
        }

        public override void Process(IEffectGiver giver, IEffectTaker taker)
        {
            if (taker == startCell as IEffectTaker)
            {
                isActive = true;
            }
            if (!isActive)
            {
                return;
            }
        }

    }
}
