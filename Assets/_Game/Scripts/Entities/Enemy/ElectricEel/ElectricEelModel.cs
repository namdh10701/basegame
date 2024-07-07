using System.Collections;
using System.Linq;
using _Base.Scripts.RPG;
using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Game.Scripts;
using _Game.Scripts.Entities;
using UnityEngine;

public class ElectricEelModel : EnemyModel
{
    [Header("Electric Eel")]
    [Space]
    public ElectricFx electricFx;
    public FindTargetBehaviour findTargetBehaviour;
    public CooldownBehaviour cooldownBehaviour;

    [SerializeField] ElectricEelProjectile Projectile;
    [SerializeField] ObjectCollisionDetector FindTargetCollider;

    public override void ApplyStats()
    {
        cooldownBehaviour.SetCooldownTime(_stats.ActionSequenceInterval.Value);
        FindTargetCollider?.SetRadius(_stats.AttackRange.Value);
    }
}
