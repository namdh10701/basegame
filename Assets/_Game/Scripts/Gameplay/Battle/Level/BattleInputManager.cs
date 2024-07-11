
using _Game.Scripts.Entities;
using UnityEngine;
using _Game.Features.Inventory;
using _Game.Scripts;
using _Base.Scripts.EventSystem;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using _Base.Scripts.Shared;

namespace _Game.Features.Gameplay
{
    public class BattleInputManager : MonoBehaviour
    {
        [SerializeField] Camera _camera;
        [SerializeField] LayerMask layerMask;
        [SerializeField] FeverOrb prefab;
        FeverOrb draggingOrb;
        bool isPointerDown;
        bool isDragging;

        GameObject pointerDownObject;
        Vector2 worldPointerPos;
        Vector2 startWorldPointerPos;

        Cannon selectingCannon;


        public ShipHUD ShipHUD;
        private void Update()
        {
#if !UNITY_EDITOR
            HandleTouch();
#else
            HandleMouse();
#endif
        }
        void OnCanvasPointerDown()
        {
            GameObject clickedObject = EventSystem.current.currentSelectedGameObject;

            if (clickedObject != null)
            {
                if (clickedObject.TryGetComponent(out FeverOrbBtn orbBtn))
                {
                    draggingOrb = Instantiate(prefab);
                    draggingOrb.transform.position = worldPointerPos;
                    draggingOrb.OnDrag(worldPointerPos);
                    return;
                }

                if (clickedObject.TryGetComponent(out AmmoButton ammoButton))
                {
                    Ammo ammo = ammoButton.ammo;
                    selectingCannon.Reload(ammo);
                    selectingCannon.border.SetActive(false);
                    ShipHUD.Hide();
                }
            }

        }

        void OnWorldPointerDown()
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(worldPointerPos, 1, Vector2.zero, Mathf.Infinity, layerMask);

            float closestDistance = Mathf.Infinity;
            RaycastHit2D closestHit = new RaycastHit2D();

            foreach (RaycastHit2D hit in hits)
            {
                float distance = Vector2.Distance(hit.point, worldPointerPos);
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
                    pointerDownObject = icd.Item;
                }
            }
        }

        void OnDrag()
        {
            if (!isPointerDown)
                return;
            if (draggingOrb != null)
            {
                draggingOrb.OnDrag(worldPointerPos);
            }
        }

        void OnWorldPointerUp()
        {
            if (!isDragging)
            {
                if (pointerDownObject != null)
                {
                    if (pointerDownObject.TryGetComponent(out Cannon cannon))
                    {
                        ShipHUD.FilterCannonUsingAmmo(cannon);
                        ShipHUD.Show();
                        selectingCannon = cannon;
                        selectingCannon.border.SetActive(true);

                        if (cannon.IsBroken)
                        {
                            GlobalEvent<IGridItem>.Send("Fix", cannon.GetComponent<IGridItem>());
                        }
                    }
                }
            }
            if (draggingOrb != null)
            {
                draggingOrb.OnDrop(worldPointerPos);
            }
        }
        void OnCanvasPointerUp()
        {
        }

        void HandleMouse()
        {
            worldPointerPos = _camera.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                isPointerDown = true;
                startWorldPointerPos = worldPointerPos;

                if (EventSystem.current.IsPointerOverGameObject())
                {
                    OnCanvasPointerDown();
                }
                else
                {
                    OnWorldPointerDown();
                }



            }

            if (isPointerDown && Vector2.Distance(startWorldPointerPos, worldPointerPos) > .1f)
            {
                isDragging = true;
            }
            if (isDragging)
            {
                OnDrag();
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    OnCanvasPointerUp();
                }
                else
                {
                    OnWorldPointerUp();
                }
                isPointerDown = false;
                isDragging = false;
                pointerDownObject = null;
            }
        }

        private bool IsPointerOverUIObject(Vector2 touchPos)
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = touchPos;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }

        void HandleTouch()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                worldPointerPos = _camera.ScreenToWorldPoint(touch.position);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        isPointerDown = true;
                        if (IsPointerOverUIObject(touch.position))
                        {
                            OnCanvasPointerDown();
                        }
                        else
                        {
                            OnWorldPointerDown();
                        }
                        break;

                    case TouchPhase.Moved:
                        OnDrag();
                        break;

                    case TouchPhase.Ended:
                        if (IsPointerOverUIObject(touch.position))
                        {
                            OnCanvasPointerDown();
                        }
                        else
                        {
                            OnWorldPointerDown();
                        }
                        isPointerDown = false;
                        isDragging = false;
                        pointerDownObject = null;
                        break;
                }
            }
        }
    }
}