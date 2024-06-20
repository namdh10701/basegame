
using _Base.Scripts.Shared;
using _Game.Scripts.Entities;
using _Game.Scripts.GameContext;
using _Game.Scripts.Gameplay.Ship;
using _Game.Scripts.Input;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Game.Scripts
{
    public class BattleInputManager : MonoBehaviour
    {
        [SerializeField] Camera _camera;
        [SerializeField] LayerMask layerMask;
        public ShipSetup shipSetup;
        Cannon selectingCannon;
        public BulletsMenu bulletMenu;
        public GameObject canvas;

        private void Update()
        {
#if UNITY_EDITOR
            HandleMouse();
#else
            HandleTouch();
#endif
        }

        void HandleMouse()
        {
            Vector3 mousePosition = _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero, Mathf.Infinity, layerMask);

            float closestDistance = Mathf.Infinity;
            RaycastHit2D closestHit = new RaycastHit2D();

            foreach (RaycastHit2D hit in hits)
            {
                float distance = Vector2.Distance(hit.point, mousePosition);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestHit = hit;
                }
            }

            // click vao Cell thi sua Cell
            // click vao Cannon thi reload cannon c

            if (closestHit.collider != null)
            {
                if (closestHit.collider.TryGetComponent(out ItemClickDetector icd))
                {
                    IWorkLocation workLocation = icd.Item.GetComponent<IWorkLocation>();
                    workLocation.OnClick();
                }
            }
        }

        bool isClick = false;
        void HandleTouch()
        {
            if (UnityEngine.Input.touchCount > 0)
            {
                Touch touch = UnityEngine.Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        isClick = true;
                        break;
                    case TouchPhase.Ended:
                        if (isClick)
                        {
                            Vector3 mousePosition = _camera.ScreenToWorldPoint(touch.position);
                            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, layerMask);
                            if (hit.collider != null)
                            {
                                if (hit.collider.TryGetComponent(out ItemClickDetector icd))
                                {
                                    IGridItem gridItem = icd.Item.gameObject.GetComponent<IGridItem>();
                                    if (gridItem is Cannon)
                                    {
                                        selectingCannon = gridItem as Cannon;
                                        CreateBulletsMenu();
                                    }
                                }
                            }
                            isClick = false;
                        }
                        break;
                }
            }
        }

        void CreateBulletsMenu()
        {
            canvas.SetActive(true);
            bulletMenu.Setup(selectingCannon, shipSetup.bullets);
        }

    }
}