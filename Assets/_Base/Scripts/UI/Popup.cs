using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class Popup : MonoBehaviour
{
    protected CanvasGroup _canvasGroup;
    [SerializeField] protected Transform _contents;
    [SerializeField] protected Button closeButton;
    [SerializeField] protected Transform background;
    protected Tween openTween;
    protected Tween closeTween;
    public bool closeByClickOnBg;

    bool initialized;
    public enum State
    {
        CLOSED, OPENED, CLOSING, OPENING
    }
    public State CurrentState { get; protected set; } = State.CLOSED;
    protected virtual void Awake()
    {
        //gameObject.SetActive(false);
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
    void Initialize()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        closeButton?.onClick.AddListener(() => PopupManager.Instance.HidePopup(this));
        if (closeByClickOnBg)
            background.GetComponent<BaseButton>().AddListener(() => PopupManager.Instance.HidePopup(this));

        openTween = _contents.transform.DOScale(1, .3f).SetEase(Ease.OutBack).SetUpdate(true);
        openTween.onComplete += () =>
        {
            _canvasGroup.blocksRaycasts = true;
            CurrentState = State.OPENED;
        };
        openTween.Pause();

        closeTween = _contents.transform.DOScale(0, .3f).SetEase(Ease.InBack).SetUpdate(true);
        closeTween.onComplete += () =>
        {
            gameObject.SetActive(false);
            CurrentState = State.CLOSED;
        };
        closeTween.Pause();
        _canvasGroup.blocksRaycasts = false;
        openTween.SetAutoKill(false);
        closeTween.SetAutoKill(false);
    }

    public void HideImmediately()
    {
        gameObject.SetActive(false);
        CurrentState = State.CLOSED;
    }

    public virtual void Show()
    {
        if (CurrentState == State.OPENED || CurrentState == State.OPENING)
        {
            return;
        }
        _contents.transform.localScale = Vector3.zero;
        CurrentState = State.OPENING;
        background.gameObject.SetActive(true);
        gameObject.SetActive(true);
        openTween.Restart();
    }

    public virtual void Hide()
    {
        if (CurrentState == State.CLOSED || CurrentState == State.CLOSING)
        {
            return;
        }

        background.gameObject.SetActive(false);
        _canvasGroup.blocksRaycasts = false;
        _contents.transform.localScale = Vector3.one;
        CurrentState = State.CLOSING;
        closeTween.Restart();
    }

    private void OnDestroy()
    {
        openTween?.Kill();
        closeTween?.Kill();
    }


}