using System.Collections;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts.Entities;
using UnityEngine;

public class ElectricEel : Enemy
{
    public ElectricEelAnimation Animation;
    public ParticleSystem ParticleSystem;
    public Projectile Projectile;
    public Transform target;
    protected override IEnumerator Start()
    {
        Animation.Attack.AddListener(DoAttack);
        return base.Start();
    }
    public override IEnumerator AttackSequence()
    {
        //Animation.ChargeExplode();
        yield return new WaitForSeconds(2);
        // Die();
        yield break;
    }

    void DoAttack(Transform t)
    {
        attack = true;
        timer = 0;
    }
    bool attack;
    public float delayTime;
    public float timer;
    private void Update()
    {
        if (attack)
        {
            timer += Time.deltaTime;
            if (timer > delayTime)
            {
                attack = false;
                ParticleSystem.Play();
                Projectile projectile = Instantiate(Projectile);
                projectile.transform.position = ParticleSystem.transform.position;
                projectile.moveSpeed.BaseValue = 10;
                projectile.transform.up = target.transform.position - ParticleSystem.transform.position;
            }
        }
    }

    public override bool IsReadyToAttack()
    {
        return false;
    }

    public override void Move()
    {
        return;
    }

    public override IEnumerator StartActionCoroutine()
    {
        yield break;
    }
}
