using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

    public class BaseButton : Button, IPointerMoveHandler
    {
        [SerializeField] private bool _isSpamable = false;
        private float _clickCooldownDuration = .5f;
        [SerializeField] private bool _allowOnlyTap = false;
        public bool IsCooldown { get; private set; }
        public Vector3 PointerDownPos { get; private set; }
        public bool IsClickedDown { get; private set; }
        public bool IsDragging { get; private set; }
        public bool IsInteracable
        {
            get
            {
                return enabled && interactable && !IsCooldown;
            }
            set
            {
                enabled = value;
            }
        }

        public void ClearListener()
        {
            onClick.RemoveAllListeners();
        }

        public void RemoveListener(UnityAction newListener)
        {
            onClick.RemoveListener(newListener);
        }

        public void RemoveAllListener()
        {
            onClick.RemoveAllListeners();
        }

        public void AddListener(UnityAction newListener)
        {
            onClick.AddListener(newListener);
        }
        public override void OnPointerDown(PointerEventData eventData)
        {
            IsClickedDown = true;
            IsDragging = false;
            PointerDownPos = eventData.position;
        }
        public bool IsLegitClick()
        {
            return _allowOnlyTap && IsDragging;
        }
        public override void OnPointerClick(PointerEventData eventData)
        {
            if (IsInteracable)
            {
                /*if (IsLegitClick())
                    return;*/
                base.OnPointerClick(eventData);
                if (!_isSpamable)
                {
                    IsCooldown = true;
                    Invoke("CompleteCooldown", _clickCooldownDuration);
                }
            }
        }

        private void CompleteCooldown()
        {
            IsCooldown = false;
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            if (IsInteracable && IsClickedDown)
            {
                if (Vector2.Distance(PointerDownPos, eventData.position) > EventSystem.current.pixelDragThreshold)
                {
                    IsDragging = true;
                }
            }
        }
    }