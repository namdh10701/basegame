using System;
using _Base.Scripts.UI.Viewx;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace _Base.Scripts.UI
{
    /// <summary>
    /// The base class for all UI elements that can be registered in UIManager
    /// </summary>

    public enum ViewType
    {
        Animator, DOTween
    }
    public abstract class View : MonoBehaviour
    {
        [SerializeField] ViewType viewType = ViewType.DOTween;
        public bool IsDestroyOnHide;

        [HideInInspector] public Action onShowStart;
        [HideInInspector] public Action onShowEnd;
        [HideInInspector] public Action onHideStart;
        [HideInInspector] public Action onHideEnd;
        [HideInInspector] public ViewState ViewState;
        [HideInInspector] public RectTransform root;
        [HideInInspector] public Vector2 originalPos;
        [HideInInspector] public CanvasGroup canvasGroup;
        [HideInInspector] public Animator animator;


        public float duration = 0;
        bool initialized;
        ICommand showCommand;
        ICommand hideCommand;

        private void Awake()
        {
            CheckInitialize();
        }
        public void CheckInitialize()
        {
            if (!initialized)
            {
                initialized = true;
                Initialize();
            }
        }
        public virtual void Initialize()
        {
            root = transform.GetChild(0).GetComponent<RectTransform>();
            originalPos = root.anchoredPosition;
            canvasGroup = GetComponent<CanvasGroup>();
            ViewState = ViewState.Hide;
            if (viewType == ViewType.DOTween)
            {
                showCommand = gameObject.AddComponent<DOTweenShow>();
                hideCommand = gameObject.AddComponent<DOTweenHide>();
            }
            else
            {
                animator = GetComponent<Animator>();
                if (animator == null)
                {
                    throw new Exception("Animator View not have animator");
                }
                showCommand = gameObject.AddComponent<AnimatorShow>();
                hideCommand = gameObject.AddComponent<AnimatorHide>();
            }
            showCommand.Initialize();
            hideCommand.Initialize();
        }

        public virtual void Show(Action onShowEnd = null)
        {
            if (ViewState == ViewState.Showing)
            {
                return;
            }
            if (ViewState == ViewState.Hiding)
            {
                hideCommand.Interrupt();
            }

            this.onShowEnd = onShowEnd;
            showCommand.Execute();
        }
        public void Hide(Action onHideEnd = null)
        {
            if (ViewState == ViewState.Hiding)
            {
                return;
            }
            if (ViewState == ViewState.Showing)
            {
                showCommand.Interrupt();
            }
            this.onHideEnd = onHideEnd;
            hideCommand.Execute();
        }

        public virtual void Deactive()
        {
            canvasGroup.blocksRaycasts = false;
        }
    }
}