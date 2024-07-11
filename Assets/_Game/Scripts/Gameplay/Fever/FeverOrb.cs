using _Base.Scripts.EventSystem;
using _Game.Features.Inventory;
using _Game.Scripts;
using _Game.Scripts.Entities;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class FeverOrb : MonoBehaviour
    {

        public LayerMask layerMask;
        public Vector2 TargetPosition;
        bool IsDragging;
        Vector3 orgPosition;
        private void Start()
        {
            orgPosition = transform.position;
        }

        public void ToggleActivate(bool IsActivate)
        {

        }

        public void OnDrag(Vector2 worldPos)
        {
            TargetPosition = worldPos;
        }
        public void OnDrop(Vector2 worldPos)
        {

            Cannon cannon = GetCannonUnderPointer(worldPos);
            if (cannon != null)
            {
                TargetPosition = cannon.transform.position;
                if (!cannon.IsBroken)
                {
                    StartCoroutine(ActivateFever(cannon));
                    ActivateFever(cannon);
                    return;
                }
            }
            Destroy(gameObject);
        }


        IEnumerator ActivateFever(Cannon cannon)
        {
            yield return transform.DOScale(1f, .5f).SetEase(Ease.OutQuad).WaitForCompletion();
            Destroy(gameObject);
            BattleManager.Instance.UseFever(cannon);
        }

        private void Update()
        {
            transform.position = Vector2.Lerp(transform.position, TargetPosition, Time.deltaTime * 8);
        }


        Cannon GetCannonUnderPointer(Vector2 pointerPos)
        {
            Cannon ret = null;
            RaycastHit2D[] hits = Physics2D.CircleCastAll(pointerPos, 2, Vector2.zero, Mathf.Infinity, layerMask);

            float closestDistance = Mathf.Infinity;
            RaycastHit2D closestHit = new RaycastHit2D();

            foreach (RaycastHit2D hit in hits)
            {
                float distance = Vector2.Distance(hit.point, pointerPos);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestHit = hit;
                }
            }

            if (closestHit.collider != null)
            {
                if (closestHit.collider.TryGetComponent(out ItemClickDetector icd))
                {
                    IWorkLocation workLocation = icd.Item.GetComponent<IWorkLocation>();
                    IGridItem gridItem = icd.Item.GetComponent<IGridItem>();
                    if (gridItem != null)
                    {
                        if (gridItem.Def.Type == ItemType.CANNON)
                        {
                            Cannon cannon = gridItem as Cannon;
                            return cannon;
                        }
                    }
                }
            }
            return ret;
        }
    }
}