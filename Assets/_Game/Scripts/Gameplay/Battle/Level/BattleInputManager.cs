
using _Game.Scripts.Entities;
using UnityEngine;
using UnityEngine.UI;
using _Game.Features.Inventory;
using _Game.Scripts.DB;
using _Game.Scripts;
using Unity.VisualScripting;
using _Base.Scripts.Shared;
using _Base.Scripts.EventSystem;
using UnityEngine.EventSystems;

namespace _Game.Features.Gameplay
{
    public class BattleInputManager : MonoBehaviour
    {
        [SerializeField] Camera _camera;
        [SerializeField] LayerMask layerMask;

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
                        Debug.Log("CLICK DOWN ALL" + hit.collider.name);
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
                            isClick = false;
                        }
                        break;
                }
            }
        }
    }
}