
using _Game.Scripts.Battle;
using _Game.Scripts.Gameplay.Ship;
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

namespace _Game.Scripts.Entities
{

    public class PufferFish : Enemy
    {
        [Header("Puffer Fish")]
        [Space]
        public PufferFishAnimation Animation;
        public TargetInRange TargetInRange;
        public PufferFishMove PufferFishMove;
        public DamageArea DamageArea;
        protected override IEnumerator Start()
        {
            Animation.OnAttack.AddListener(DoAttack);
            Ship Ship = FindAnyObjectByType<Ship>();
            TargetInRange.TargetShip = Ship;
            Vector2 targetPos = Ship.EffectCollider.ClosestPoint(transform.position);
            PufferFishMove.direction.BaseValue = (targetPos - (Vector2)transform.position).normalized;
            yield return base.Start();
        }
        bool isAttacking;
        public override IEnumerator AttackSequence()
        {
            if (!isAttacking)
            {
                isAttacking = true;
                Animation.ChargeExplode();
                yield return new WaitForSeconds(2);
                Die();
                Animation.PlayDie(() =>
                {
                    Destroy(gameObject);
                });

            }
        }


        public override bool IsReadyToAttack()
        {
            return true && TargetInRange.IsMet;
        }

        public override void Move()
        {
            PufferFishMove.Move();
        }

        public override IEnumerator StartActionCoroutine()
        {
            transform.localScale = Vector3.zero;
            yield return new WaitForSeconds(1);
            Tween tween = transform.DOScale(1, .5f).SetEase(Ease.OutBack);
            yield return tween.WaitForCompletion();
        }
        public override void Die()
        {
            base.Die();
            StartCoroutine(AttackSequence());
        }

        public void DoAttack()
        {
            DamageArea da = Instantiate(DamageArea, transform.position, Quaternion.identity);
            da.SetDamage(_stats.AttackDamage.Value);
        }
    }
}