
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


        public Ship Ship;
        public ShipHUD ShipHUD;
        private void Start()
        {
            _camera = Camera.main;
        }
        private void Update()
        {
            HandleTouch();
            //HandleMouse();

        }
        void OnCanvasPointerDown()
        {
            GameObject clickedObject = EventSystem.current.currentSelectedGameObject;

            if (clickedObject != null)
            {
                Debug.Log("CLICKED"); Debug.Log(clickedObject.name);
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
                    if (ammo.IsBroken)
                    {
                        return;
                    }
                    ShipStats shipStats = Ship.Stats as ShipStats;

                    if (shipStats.ManaPoint.Value >= ammo.stats.EnergyCost.Value)
                    {
                        shipStats.ManaPoint.StatValue.BaseValue -= ammo.stats.EnergyCost.Value;
                        GlobalEvent<int, Vector3>.Send("MANA_CONSUMED", (int)ammo.stats.EnergyCost.Value, worldPointerPos);
                    }
                    selectingCannon.Reload(ammo);
                    DeselectCannon();
                    ShipHUD.Hide();
                    return;
                }

                ShipHUD.Hide();
                DeselectCannon();
            }
            else
            {
                ShipHUD.Hide();
                DeselectCannon();
            }

        }

        void DeselectCannon()
        {
            if (selectingCannon != null)
            {
                selectingCannon.border.SetActive(false);
                selectingCannon = null;
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
            else
            {
                ShipHUD.Hide();
                DeselectCannon();
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
                    Debug.Log(pointerDownObject.name);
                    if (pointerDownObject.TryGetComponent(out Cannon cannon))
                    {
                        DeselectCannon();
                        ShipHUD.FilterCannonUsingAmmo(cannon);
                        ShipHUD.Show();
                        selectingCannon = cannon;
                        selectingCannon.border.SetActive(true);

                        if (cannon.IsBroken)
                        {
                            GlobalEvent<Cannon, int>.Send("FixCannon", cannon, int.MaxValue);
                        }
                    }
                    if (pointerDownObject.TryGetComponent(out Ammo ammo))
                    {
                        Debug.Log("click mmo");

                        if (ammo.IsBroken)
                        {
                            GlobalEvent<Cannon, int>.Send("FixAmmo", cannon, int.MaxValue);
                        }
                    }
                    if (pointerDownObject.TryGetComponent(out Cell cell))
                    {
                        if (cell.isBroken)
                        {

                            Debug.Log("send");
                            GlobalEvent<Cell, int>.Send("FixCell", cell, int.MaxValue);
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
            if (draggingOrb != null)
            {
                draggingOrb.OnDrop(worldPointerPos);
            }
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
                Debug.Log("TOUCHED");
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        isPointerDown = true;
                        if (IsPointerOverUIObject(touch.position))
                        {
                            Debug.Log("TOUCHED canvas");
                            OnCanvasPointerDown();
                        }
                        else
                        {
                            Debug.Log("TOUCHED world");
                            Debug.Log(worldPointerPos);
                            OnWorldPointerDown();
                        }
                        break;

                    case TouchPhase.Moved:
                        isDragging = true;
                        OnDrag();
                        break;

                    case TouchPhase.Ended:
                        if (IsPointerOverUIObject(touch.position))
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
                        break;
                }
            }
        }
    }
}