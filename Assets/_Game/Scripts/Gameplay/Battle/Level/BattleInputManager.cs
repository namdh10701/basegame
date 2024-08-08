
using _Game.Scripts.Entities;
using UnityEngine;
using _Game.Features.Inventory;
using _Game.Scripts;
using _Base.Scripts.EventSystem;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using _Base.Scripts.Shared;
using _Game.Scripts.Battle;

namespace _Game.Features.Gameplay
{
    public class BattleInputManager : MonoBehaviour
    {

        [SerializeField] Camera _camera;
        [SerializeField] LayerMask layerMask;
        bool isPointerDown;
        bool isDragging;

        GameObject pointerDownObject;
        public Vector2 worldPointerPos;
        Vector2 startWorldPointerPos;

        Cannon selectingCannon;


        public EntityManager EntityManager;
        public ReloadCannonController reloadCannonController;
        public UseFeverController useFeverController;
        public CrewJobData CrewJobData;
        private void Start()
        {
            _camera = Camera.main;
        }
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
                    useFeverController.StartDrag(worldPointerPos);
                    return;
                }

                if (clickedObject.TryGetComponent(out AmmoButton ammoButton))
                {
                    Ammo ammo = ammoButton.ammo;
                    if (ammo.GridItemStateManager.GridItemState == GridItemState.Broken)
                    {
                        return;
                    }
                    //TODO: RELOAD AMMO BUFF HERE
                    reloadCannonController.ReloadCannon(EntityManager.Ship, selectingCannon, ammo);
                    DeselectCannon();
                    EntityManager.Ship.HUD.Hide();
                    return;
                }

                EntityManager.Ship.HUD.Hide();
                DeselectCannon();
            }
            else
            {
                EntityManager.Ship.HUD.Hide();
                DeselectCannon();
            }

        }

        void DeselectCannon()
        {
            if (selectingCannon != null)
            {
                selectingCannon.View.Border.SetActive(false);
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
                EntityManager.Ship.HUD.Hide();
                DeselectCannon();
            }
        }

        void OnDrag()
        {
            if (!isPointerDown)
                return;
            useFeverController.OnDrag(worldPointerPos);
        }

        void OnWorldPointerUp()
        {
            if (!isDragging)
            {
                if (pointerDownObject != null)
                {
                    if (pointerDownObject.TryGetComponent(out Cannon cannon))
                    {
                        if (cannon.GridItemStateManager.GridItemState == GridItemState.Broken)
                        {
                            if (CrewJobData.FixCannonJobDic[cannon].Status == TaskStatus.Pending)
                            {
                                DeselectCannon();
                                EntityManager.Ship.HUD.FilterCannonUsingAmmo(cannon);
                                EntityManager.Ship.HUD.Show();
                                selectingCannon = cannon;
                                selectingCannon.View.Border.SetActive(true);
                            }
                            else
                            {
                                GlobalEvent<Cannon, int>.Send("FixCannon", cannon, int.MaxValue);
                            }
                        }
                        else
                        {
                            DeselectCannon();
                            if (cannon.FeverState == CannonFeverState.None)
                            {
                                EntityManager.Ship.HUD.FilterCannonUsingAmmo(cannon);
                                EntityManager.Ship.HUD.Show();
                                selectingCannon = cannon;
                                selectingCannon.View.Border.SetActive(true);
                            }
                        }
                    }
                    if (pointerDownObject.TryGetComponent(out Ammo ammo))
                    {
                        Debug.Log("click mmo");

                        if (ammo.GridItemStateManager.GridItemState == GridItemState.Broken)
                        {
                            GlobalEvent<Cannon, int>.Send("FixAmmo", cannon, int.MaxValue);
                        }
                    }
                    if (pointerDownObject.TryGetComponent(out Cell cell))
                    {
                        if (cell.isBroken)
                        {
                            GlobalEvent<Cell, int>.Send("FixCell", cell, int.MaxValue);
                        }
                    }

                    /*if (pointerDownObject.TryGetComponent(out Carpet carpet))
                    {
                        if (carpet.GridItemStateManager.GridItemState == GridItemState.Broken)
                        {
                            GlobalEvent<Carpet, int>.Send("FixCarpet", carpet, int.MaxValue);
                        }
                    }*/
                }
            }

            useFeverController.OnDrop(worldPointerPos);
        }
        void OnCanvasPointerUp()
        {
            useFeverController.OnDrop(worldPointerPos);
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