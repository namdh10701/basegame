using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

    [RequireComponent(typeof(BaseButton))]
    public class AnimatedButton : UIBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private BaseButton _baseButton;
        [SerializeField] private Transform _transform;
        Tween downTween;
        Tween upTween;
        bool isClickedDown = false;

        protected override void Awake()
        {
            base.Awake();
            _baseButton = GetComponent<BaseButton>();
            if (_transform is null)
            {
                _transform = GetComponent<Transform>();
            }
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            if (_baseButton.IsInteracable)
            {
                isClickedDown = true;
                upTween.Complete();
                downTween = _transform.DOScale(.9f, .1f);
            }
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            if (isClickedDown)
            {
                isClickedDown = false;
                downTween.Complete();
                upTween = _transform.DOScale(1f, .1f);
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }

