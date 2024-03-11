using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
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
    Command showCommand;
    Command hideCommand;

    private void Awake()
    {
        CheckInitialize();
    }
    void CheckInitialize()
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
        ViewState = ViewState.HIDE;
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
    }

    public virtual void Show(Action onShowEnd = null)
    {
        if (ViewState == ViewState.SHOWING)
        {
            return;
        }
        if (ViewState == ViewState.HIDING)
        {
            hideCommand.Interupt();
        }
        this.onShowEnd = onShowEnd;
        showCommand.Execute();
    }
    public void Hide(Action onHideEnd = null)
    {
        if (ViewState == ViewState.HIDING)
        {
            return;
        }
        if (ViewState == ViewState.SHOWING)
        {
            showCommand.Interupt();
        }
        this.onHideEnd = onHideEnd;
        hideCommand.Execute();
    }

    public virtual void Deactive()
    {
        canvasGroup.blocksRaycasts = false;
    }
}
