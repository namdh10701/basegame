using System.Collections;
using System.Collections.Generic;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts;
using _Game.Scripts.Entities;
using UnityEngine;

public class ElectricEel : Enemy
{
    [Header("Electric Eel")]

    public ElectricEelAnimation Animation;
    public ElectricFx electricFx;
    public ElectricEelProjectile Projectile;
    public Transform target;
    public CooldownBehaviour CooldownBehaviour;
    [Header("Sycn Animation")]

    public float delayTime;
    public float timer;
    bool attack;
    protected override IEnumerator Start()
    {
        Animation.Attack.AddListener(DoAttack);
        Animation.OnHide += OnHide;
        MoveAreaController moveArea = FindAnyObjectByType<MoveAreaController>();
        blackboard.GetVariable<AreaVariable>("MoveArea").Value = moveArea.GetArea(AreaType.Floor2Plus3);
        return base.Start();
    }

    void OnHide()
    {
        pushCollider.enabled = false;
        EffectTakerCollider.enabled = false;
    }

    public override IEnumerator AttackSequence()
    {
        FindTarget();
        Animation.Charge();
        yield return new WaitForSeconds(2);
        Animation.PlayAttack();
        CooldownBehaviour.StartCooldown();
        yield break;
    }

    void FindTarget()
    {

    }

    void DoAttack()
    {
        attack = true;
        timer = 0;
    }
    private void Update()
    {
        if (attack)
        {
            timer += Time.deltaTime;
            if (timer > delayTime)
            {
                attack = false;
                electricFx.targetTransform = target;
                electricFx.Play();
                ElectricEelProjectile projectile = Instantiate(Projectile);
                projectile.transform.position = electricFx.transform.position;
                projectile.targetTransform = target;
                projectile.startTransform = transform;
                ((HomingMove)projectile.ProjectileMovement).target = target;
            }
        }
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
        attack = false;
        Animation.PlayDie(() => { Destroy(gameObject); });
    }
    public override IEnumerator StartActionCoroutine()
    {
        pushCollider.enabled = false;
        Animation.Appear();
        yield return new WaitForSeconds(1.5f);
        pushCollider.enabled = true;
        CooldownBehaviour.SetCooldownTime(7);
        CooldownBehaviour.StartCooldown();
    }

    public void Hide()
    {
        Animation.Hide();
    }

    public void Show()
    {
        Animation.Appear();
        pushCollider.enabled = true;
        EffectTakerCollider.enabled = true;
    }
}
