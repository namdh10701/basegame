using _Game.Scripts.Entities;
using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PufferFishAnimation : SpineAnimationEnemyHandler
{
    protected override void Start()
    {
        base.Start();
    }

    public void ChargeExplode()
    {
        PlayAnimSequence("bomb_ship_transform", "bomb_ship_loop", true);
    }
    public void Explode()
    {
        PlayAnim("bomb_ship_explo", false);
    }
}
