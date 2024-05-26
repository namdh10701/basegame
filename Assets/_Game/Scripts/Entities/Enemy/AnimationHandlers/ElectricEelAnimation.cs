using _Game.Scripts.Entities;
using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricEelAnimation : SpineAnimationEnemyHandler
{
    protected override void Start()
    {
        base.Start();
    }

    public void ChargeExplode()
    {
        PlayAnim("attack", false);
        
    }
}
