
using _Game.Scripts.Entities;
using UnityEngine;
using _Game.Features.Inventory;
using _Game.Scripts;
using _Base.Scripts.EventSystem;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace _Game.Features.Gameplay
{
    public class BattleInputManager : MonoBehaviour
    {
        [SerializeField] Camera _camera;
        [SerializeField] LayerMask layerMask;
        [SerializeField] FeverOrb prefab;

        FeverOrb draggingOrb;

        bool clicked;
        private void Update()
        {
            HandleTouch();
        }

        void HandleMouse()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {

                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    Vector3 mousePosition = _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                    RaycastHit2D[] hits = Physics2D.CircleCastAll(mousePosition, 1, Vector2.zero, Mathf.Infinity, layerMask);

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

                    if (closestHit.collider != null)
                    {
                        OnClickDown(closestHit.collider);
                    }
                    else
                    {
                        GlobalEvent.Send("CloseHUD");
                    }
                }
                else
                {
                    GameObject clickedObject = EventSystem.current.currentSelectedGameObject;

                    if (clickedObject != null && clickedObject.name == "FeverOrbBtn")
                    {
                        draggingOrb = Instantiate(prefab);
                        draggingOrb.transform.position = _camera.ScreenToWorldPoint(Input.mousePosition);
                    }
                }
                clicked = true;
            }
            if (clicked)
            {
                if (draggingOrb != null)
                {
                    draggingOrb.OnDrag(_camera.ScreenToWorldPoint(Input.mousePosition));
                }

            }

            if (Input.GetMouseButtonUp(0))
            {
                if (clicked && draggingOrb != null)
                {
                    clicked = false;
                    draggingOrb.OnDrop(_camera.ScreenToWorldPoint(Input.mousePosition));
                }
            }
        }

        void OnClickDown(Collider2D collider)
        {
            if (collider.TryGetComponent(out ItemClickDetector icd))
            {
                IWorkLocation workLocation = icd.Item.GetComponent<IWorkLocation>();
                workLocation.OnClick();
                IGridItem gridItem = icd.Item.GetComponent<IGridItem>();
                if (gridItem != null)
                {
                    if (gridItem.Def.Type == ItemType.CANNON)
                    {
                        Cannon cannon = gridItem as Cannon;
                        GlobalEvent<Cannon>.Send("ClickCannon", cannon);
                    }
                    else
                    {
                        GlobalEvent.Send("CloseHUD");
                    }
                }
                else
                {
                    GlobalEvent.Send("CloseHUD");
                }
            }
            else
            {
                GlobalEvent.Send("CloseHUD");
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
        bool isClick = false;
        bool isdragging;
        float clickTimer = 0;
        bool clickOnUI;
        void HandleTouch()
        {
            if (isClick)
            {
                clickTimer += Time.deltaTime;
            }
            if (UnityEngine.Input.touchCount > 0)
            {
                Touch touch = UnityEngine.Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        isClick = true;
                        isdragging = false;
                        if (IsPointerOverUIObject(touch.position))
                        {
                            clickOnUI = true;
                            GameObject clickedObject = EventSystem.current.currentSelectedGameObject;

                            if (clickedObject != null && clickedObject.name == "FeverOrbBtn")
                            {
                                draggingOrb = Instantiate(prefab);
                                draggingOrb.transform.position = _camera.ScreenToWorldPoint(touch.position);
                                draggingOrb.OnDrag(draggingOrb.transform.position);
                            }
                        }
                        break;


                    case TouchPhase.Ended:
                        if (isClick)
                        {
                            if (draggingOrb != null)
                            {
                                draggingOrb.OnDrop(_camera.ScreenToWorldPoint(touch.position));
                            }
                        }
                        if (isClick && !isdragging)
                        {
                            Vector3 mousePosition = _camera.ScreenToWorldPoint(touch.position);
                            RaycastHit2D[] hits = Physics2D.CircleCastAll(mousePosition, 1, Vector2.zero, Mathf.Infinity, layerMask);

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

                            if (closestHit.collider != null)
                            {
                                OnClickDown(closestHit.collider);
                            }
                            else
                            {
                                if (!clickOnUI)
                                    GlobalEvent.Send("CloseHUD");
                            }
                            isdragging = false;
                            clickOnUI = false;
                            isClick = false;
                            clickTimer = 0;
                        }
                        break;
                    case TouchPhase.Moved:
                        isdragging = true;
                        if (draggingOrb != null)
                        {
                            draggingOrb.OnDrag(_camera.ScreenToWorldPoint(touch.position));
                        }
                        break;
                }
            }
        }
    }
}