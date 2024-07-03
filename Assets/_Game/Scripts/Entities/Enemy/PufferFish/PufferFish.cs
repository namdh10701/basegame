using _Game.Scripts.Gameplay.Ship;
using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace _Game.Scripts.Entities
{

    public class PufferFish : Enemy
    {
        [Header("Puffer Fish")]
        [Space]

        PufferFishAnimation Animation;
        public PufferFishMove PufferFishMove;
        public DamageArea DamageArea;
        protected override IEnumerator Start()
        {
            Animation = (PufferFishAnimation)spineAnimationEnemyHandler;
            Animation.OnAttack.AddListener(DoAttack);
            yield return base.Start();
        }
        bool isAttacking;
        public override IEnumerator AttackSequence()
        {
            if (!isAttacking)
            {
                MBTExecutor.enabled = false;
                body.velocity = Vector2.zero;
                Invoke("ReducePoise", .3f);
                Destroy(PufferFishMove);
                isAttacking = true;
                Animation.ChargeExplode();
                yield return new WaitForSeconds(2);

                Animation.PlayDie(() =>
                {
                    base.Die();
                    Destroy(gameObject);
                });

            }
        }

        void ReducePoise()
        {
            _stats.Poise.BaseValue = 0;
        }


        public override bool IsReadyToAttack()
        {
            return true;
        }

        public override void Move()
        {
            PufferFishMove?.Move();
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
            Destroy(PufferFishMove);
            body.velocity = Vector2.zero;
            StartCoroutine(AttackSequence());
        }

        public void DoAttack()
        {
            pushCollider.gameObject.SetActive(false);
            EffectTakerCollider.gameObject.SetActive(false);
            DamageArea da = Instantiate(DamageArea, transform.position, Quaternion.identity);
            da.SetDamage(_stats.AttackDamage.Value, 0);
        }
    }
}