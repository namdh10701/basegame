using _Base.Scripts.EventSystem;
using _Game.Features.Battle;
using _Game.Features.Gameplay;
using _Game.Scripts;
using _Game.Scripts.Battle;
using _Game.Scripts.Entities;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class UseFeverController : MonoBehaviour
{
    public LayerMask clickDetectorLayerMask;
    public FeverOrb feverOrbPrefab;

    FeverOrb draggingOrb;
    public FeverSpeedFx FeverSpeedFx;
    public EntityManager EntityManager;
    private void Awake()
    {
        GlobalEvent.Register("UseFullFever", UseFullFever);
    }
    private void OnDestroy()
    {
        GlobalEvent.Unregister("UseFullFever", UseFullFever);
    }
    public void StartDrag(Vector2 worldPointerPos)
    {
        draggingOrb = Instantiate(feverOrbPrefab);
        draggingOrb.transform.position = worldPointerPos;
        draggingOrb.TargetPosition = worldPointerPos;
    }

    public void OnDrag(Vector2 worldPos)
    {
        if (draggingOrb == null)
            return;
        draggingOrb.TargetPosition = worldPos;
    }
    public void OnDrop(Vector2 worldPos)
    {
        if (draggingOrb == null)
            return;

        Cannon cannon = GetCannonUnderPointer(worldPos);
        if (cannon != null)
        {
            draggingOrb.TargetPosition = cannon.transform.position;
            if (cannon.GridItemStateManager.GridItemState == GridItemState.Active)
            {
                //TODO FEVER BUFF GO HERE
                StartCoroutine(ActivateFever(cannon));
                return;
            }
        }
        Destroy(draggingOrb.gameObject);
    }

    IEnumerator ActivateFever(Cannon cannon)
    {
        yield return transform.DOScale(1f, .5f).SetEase(Ease.OutQuad).WaitForCompletion();
        Destroy(draggingOrb.gameObject);
        UseFever(cannon);
    }

    Cannon GetCannonUnderPointer(Vector2 pointerPos)
    {
        Cannon ret = null;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(pointerPos, 2, Vector2.zero, Mathf.Infinity, clickDetectorLayerMask);

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
                IGridItem gridItem = icd.Item.GetComponent<IGridItem>();
                if (gridItem != null)
                {
                    if (gridItem is Cannon cannon)
                    {
                        return cannon;
                    }
                }
            }
        }
        return ret;
    }


    public void UseFever(Cannon cannon)
    {
        if (cannon.UsingAmmo == null)
        {
            return;
        }
        ShipStats shipStats = EntityManager.Ship.Stats as ShipStats;
        if (shipStats.Fever.Value < 200)
            return;
        shipStats.Fever.StatValue.BaseValue -= 200;
        cannon.OnFeverEffectEnter();
    }
    public void UseFullFever()
    {
        StartCoroutine(UseFullFeverCoroutine());
    }

    IEnumerator UseFullFeverCoroutine()
    {
        ShipStats shipStats = EntityManager.Ship.Stats as ShipStats;

        float decreaseDuration = 10;
        float decreaseRate = shipStats.Fever.MaxValue / decreaseDuration;

        FeverSpeedFx.Activate();
        EntityManager.Ship.EnterFullFever();

        yield return new WaitForSeconds(10);
        EntityManager.Ship.ExitFullFever();
        FeverSpeedFx.Deactivate();
    }
}
