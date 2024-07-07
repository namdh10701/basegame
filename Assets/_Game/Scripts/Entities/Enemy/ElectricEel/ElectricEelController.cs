using _Base.Scripts.RPG;
using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts;
using _Game.Scripts.Entities;
using MBT;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ElectricEelController : EnemyController
{
    ElectricEelAnimation Animation;
    [SerializeField] ElectricFx electricFx;
    [SerializeField] ElectricEelProjectile Projectile;
    [SerializeField] Transform target;

    [Header("Sycn Animation")]
    [SerializeField] float syncAnimationPlayFxTime = .5f;
    [SerializeField] float syncAnimationSpawnProjectileTime = .8f;

    FindTargetBehaviour findTargetBehaviour;
    CooldownBehaviour cooldownBehaviour;

    public override void Initialize(EnemyModel enemyModel, EffectTakerCollider effectTakerCollider, Blackboard blackboard, MBTExecutor mbtExecutor, Rigidbody2D body, SpineAnimationEnemyHandler anim)
    {
        this.electricFx = ((ElectricEelModel)enemyModel).electricFx;
        this.findTargetBehaviour = ((ElectricEelModel)enemyModel).findTargetBehaviour;
        this.cooldownBehaviour = ((ElectricEelModel)enemyModel).cooldownBehaviour;
        Animation = anim as ElectricEelAnimation;
        Animation.Attack.AddListener(DoAttack);
        Animation.OnHide += OnHide;
        MoveAreaController moveArea = FindAnyObjectByType<MoveAreaController>();
        blackboard.GetVariable<AreaVariable>("MoveArea").Value = moveArea.GetArea(AreaType.All);
        base.Initialize(enemyModel, effectTakerCollider, blackboard, mbtExecutor, body, anim);
    }
    void DoAttack()
    {
        Invoke("PlayAttackFx", syncAnimationPlayFxTime);
        Invoke("SpawnProjectile", syncAnimationSpawnProjectileTime);
    }

    void PlayAttackFx()
    {
        electricFx.targetTransform = target;
        electricFx.Play();
    }

    void SpawnProjectile()
    {
        ElectricEelProjectile projectile = Instantiate(Projectile);
        projectile.transform.position = electricFx.transform.position;
        projectile.targetTransform = target;
        projectile.startTransform = transform;
        projectile.targetTransform = target;
    }
    void OnHide()
    {
        effectTakerCollider.enabled = false;
    }

    public override IEnumerator AttackSequence()
    {
        if (findTargetBehaviour.MostTargets.Count == 0)
        {
            yield break;
        }
        else
        {
            Crew crew = findTargetBehaviour.MostTargets.First() as Crew;
            target = crew.EffectTakerCollider.transform;
        }
        Animation.Charge();
        yield return new WaitForSeconds(2);
        Animation.PlayAttack();
        cooldownBehaviour.StartCooldown();
        yield break;

    }

    public void Hide()
    {
        Animation.Hide();
        cooldownBehaviour.gameObject.SetActive(false);
    }

    public void Show()
    {
        Animation.Appear();
        cooldownBehaviour.gameObject.SetActive(true);
    }

    public override void Die()
    {
        base.Die();
        StopAllCoroutines();
        CancelInvoke();
        Animation.PlayDie(() => { Destroy(gameObject); });
    }

    public override bool IsReadyToAttack()
    {
        return !cooldownBehaviour.IsInCooldown && findTargetBehaviour.MostTargets.Count > 0;
    }

    public override IEnumerator StartActionCoroutine()
    {
        yield break;
    }
}
