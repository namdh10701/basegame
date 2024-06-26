using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts;
using _Game.Scripts.Battle;
using _Game.Scripts.Entities;
using MBT;
using UnityEngine;

public class ElectricEel : Enemy
{
    [Header("Electric Eel")]

    [SerializeField] ElectricEelAnimation Animation;
    [SerializeField] ElectricFx electricFx;
    [SerializeField] ElectricEelProjectile Projectile;
    [SerializeField] Transform target;
    [SerializeField] CooldownBehaviour CooldownBehaviour;
    [SerializeField] FindTargetBehaviour FindTargetBehaviour;

    [Header("Sycn Animation")]
    [SerializeField] float syncAnimationPlayFxTime = .5f;
    [SerializeField] float syncAnimationSpawnProjectileTime = .8f;
    protected override IEnumerator Start()
    {
        Animation.Attack.AddListener(DoAttack);
        Animation.OnHide += OnHide;
        MoveAreaController moveArea = FindAnyObjectByType<MoveAreaController>();
        blackboard.GetVariable<AreaVariable>("MoveArea").Value = moveArea.GetArea(AreaType.All);
        yield return base.Start();
    }

    void OnHide()
    {
        pushCollider.enabled = false;
        EffectTakerCollider.enabled = false;
    }

    public override IEnumerator AttackSequence()
    {
        if (FindTargetBehaviour.MostTargets.Count == 0)
        {
            yield break;
        }
        else
        {
            Crew crew = FindTargetBehaviour.MostTargets.First() as Crew;
            target = crew.EffectTakerCollider.transform;
        }
        Animation.Charge();
        yield return new WaitForSeconds(2);
        Animation.PlayAttack();
        CooldownBehaviour.StartCooldown();
        yield break;

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
        ((HomingMove)projectile.ProjectileMovement).target = target;
    }

    public override bool IsReadyToAttack()
    {
        return !CooldownBehaviour.IsInCooldown;
    }

    public override void Move()
    {
        return;
    }

    public override void Die()
    {
        base.Die();
        StopAllCoroutines();
        CancelInvoke();
        Animation.PlayDie(() => { Destroy(gameObject); });
    }
    public override IEnumerator StartActionCoroutine()
    {
        effectHandler.enabled = false;
        pushCollider.enabled = false;
        Animation.Appear();
        yield return new WaitForSeconds(1.5f);
        CooldownBehaviour.SetCooldownTime(7);
        CooldownBehaviour.StartCooldown();
    }

    public void Hide()
    {
        Animation.Hide();
        pushCollider.enabled = false;
        EffectTakerCollider.gameObject.SetActive(false);
    }

    public void Show()
    {
        Animation.Appear();
        pushCollider.enabled = true;
        EffectTakerCollider.gameObject.SetActive(true);
    }
}
