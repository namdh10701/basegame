using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Game.Features.Gameplay;
using _Game.Scripts.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class CannonView : MonoBehaviour, IGridItemView
    {
        [SerializeField] MeshRenderer mainRenderer;
        [SerializeField] Transform refer;
        [SerializeField] ICannonVisualElement[] cannonVisualElements;
        [SerializeField] Transform sortPivot;

        public SpineAnimationCannonHandler Animation;
        Cannon cannon;
        public CannonHUD cannonHUD;
        public GameObject Border;

        bool isActive = true;
        bool isOutOfAmmo;
        bool isStunned;


        public void OnStunStatusChanged(bool isStunned)
        {
            this.isStunned = isStunned;
        }

        public void HandleActive()
        {
            isActive = true;
            UpdateView();
        }

        public void HandleBroken()
        {
            isActive = false;
            UpdateView();
        }


        public void Init(IGridItem gridItem)
        {
            cannon = gridItem as Cannon;
            cannon.CannonAmmo.OutOfAmmoStateChaged += OutOfAmmoStateChanged;
            cannon.OnStunStatusChanged += OnStunStatusChanged;
            cannonHUD.SetCannon(cannon);
            Border.SetActive(false);
            cannonVisualElements = GetComponentsInChildren<ICannonVisualElement>();
        }

        private void OutOfAmmoStateChanged(Cannon cannon, bool isOutOfAmmo)
        {
            this.isOutOfAmmo = isOutOfAmmo;
            UpdateView();
        }

        void UpdateView()
        {
            if (!isOutOfAmmo && isActive && !isStunned)
            {
                Animation.PlayNormal();
            }
            else
            {
                Animation.PlayBroken();
            }
        }

        void Update()
        {
            if (refer.rotation.eulerAngles.z > 90 && refer.rotation.eulerAngles.z < 270)
            {
                mainRenderer.sortingLayerName = "AboveShipFront";
            }
            else
            {
                mainRenderer.sortingLayerName = "OnShip";
            }

            mainRenderer.sortingOrder = Mathf.RoundToInt(sortPivot.position.y * -100);
            foreach (ICannonVisualElement cannonVisualElement in cannonVisualElements)
            {
                cannonVisualElement.UpdateSorting(mainRenderer);
            }
        }
    }
}