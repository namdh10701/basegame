using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class BodyPartModel : PartModel
    {
        public LaserGuide laserGuide;
        Sequence sequence;
        Cell startCell;
        bool isLeft;
        [SerializeField] Vector3 orgPos;

        public Action AttackStarted;
        public Action AttackEnded;
        public bool IsAttacking;

        public override void Initialize(GiantOctopus giantOctopus)
        {
            base.Initialize(giantOctopus);
            gridPicker = FindAnyObjectByType<GridPicker>();
        }
        public void Attack()
        {
            State = PartState.Attacking;
        }
        public override void AfterStun()
        {
            State = PartState.Idle;
        }
        public override void DoAttack()
        {
            base.DoAttack();
            if (sequence != null)
                return;
            sequence = DOTween.Sequence();
            startCell = gridPicker.PickRandomCell();
            Debug.Log(gridPicker);
            if (startCell.transform.position.x < gridPicker.ShipGrid.transform.position.x)
            {
                isLeft = true;
            }
            else
            {
                isLeft = false;
            }
            Vector3 startPos;
            Vector3 endPos;
            if (isLeft)
            {
                Debug.LogError("LEFT");
                startPos = gridPicker.ShipGrid.Ship.ShipBound.ClosetPointToRight(startCell.transform.position) + new Vector2(1, 0);
                endPos = gridPicker.ShipGrid.Ship.ShipBound.ClosetPointToLeft(startPos) + new Vector2(-1, 0);
            }
            else
            {
                Debug.LogError("RIGHT");
                startPos = gridPicker.ShipGrid.Ship.ShipBound.ClosetPointToLeft(startCell.transform.position) + new Vector2(-1, 0);
                endPos = gridPicker.ShipGrid.Ship.ShipBound.ClosetPointToRight(startPos) + new Vector2(1, 0);
            }

            IsAttacking = true;
            laserGuide.Initialize(startCell, isLeft);
            sequence.Append(laserGuide.transform.DOMove(startPos, 1));
            sequence.AppendCallback(() => laserGuide.gameObject.SetActive(true));
            sequence.Append(laserGuide.transform.DOMove(endPos, 1));
            sequence.AppendCallback(() => laserGuide.gameObject.SetActive(false));
            sequence.Append(laserGuide.transform.DOMove(orgPos, 1));
            sequence.AppendCallback(() => StopAttack());
        }

        void StopAttack()
        {
            sequence = null;
            IsAttacking = false;
            AttackEnded?.Invoke();
        }
    }
}