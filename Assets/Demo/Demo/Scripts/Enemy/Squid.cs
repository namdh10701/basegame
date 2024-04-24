using _Game.Scripts.Battle;
using Demo.Scripts.Canon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Scripts.Enemy
{
    public class Squid : Enemy
    {
        [SerializeField] Projectile bulletPrefab;
        [SerializeField] Transform shootPos;
        bool isMoved = false;
        Coroutine moveCoroutine;
        Area moveArea;
        protected override void Start()
        {
            base.Start();
            moveArea = GameObject.FindGameObjectWithTag("MoveArea").GetComponent<Area>();
        }
        public override bool IsAbleToAttack
        {
            get
            {
                return !Cooldown.IsInCooldown && isMoved;
            }
        }
        protected override void Update()
        {
            Debug.Log("Enter here 1");
            if (!isMoved && moveCoroutine == null)
            {
                Debug.Log("Enter here");
                moveCoroutine = StartCoroutine(MoveCoroutine());
            }

            base.Update();
        }

        IEnumerator MoveCoroutine()
        {
            Vector2 point = moveArea.SamplePoint();
            float elapsedTime = 0;
            float duration = 2;
            Vector2 startPos = body.position;
            while
                (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                Vector2 pos = Vector2.Lerp(startPos, point, elapsedTime / duration);
                body.MovePosition(pos);
                yield return null;
            }
            isMoved = true;
            yield return new WaitForSeconds(1.5f);
            moveCoroutine = null;
        }

        public override void DoAttack()
        {
            base.DoAttack();
            isMoved = false;
            Projectile projectile = Instantiate(bulletPrefab, shootPos.position, Quaternion.identity);
            projectile.transform.up = (centerCell.transform.position - shootPos.position).normalized;
            projectile.targetCells = targetCells;
            projectile.centerCell = centerCell;


        }

        public override void DoTarget()
        {
            targetCells = gridPicker.PickCells(transform, CellPickType.RandomCell, cellPattern, 2, out centerCell);
            gridAttackHandler.PlayTargetingFx(targetCells);
        }
    }
}