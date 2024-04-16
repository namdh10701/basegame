using DG.Tweening;
using UnityEngine;

namespace _Base.Scripts.UI.Viewx
{
    public class DOTweenHide : MonoBehaviour, ICommand
    {
        View view;
        DG.Tweening.Tween hideTween;
        bool initialized;
        private void Awake()
        {
            Initialize();
        }
        public void Initialize()
        {
            if (!initialized)
            {
                initialized = true;
                Init();
            }
        }
        public void Init()
        {
            view = GetComponent<View>();

            hideTween = DOTweenModuleUI.DOFade((CanvasGroup)view.canvasGroup, 0, view.duration / 2).OnPlay(() =>
            {
                view.canvasGroup.blocksRaycasts = false;
            }).OnComplete(() =>
            {
                view.root.anchoredPosition = view.originalPos;
                this.OnCompleted();
            });
            hideTween.SetAutoKill(false);
            hideTween.Pause();
        }
        public void Execute()
        {
            view.canvasGroup.alpha = 1;
            hideTween.Restart();
            view.onHideStart?.Invoke();
            view.ViewState = ViewState.Hiding;
        }

        public void Interrupt()
        {
            hideTween.Pause();
        }

        public void OnCompleted()
        {
            view.onHideEnd?.Invoke();
            view.ViewState = ViewState.Hide;
        }

        private void OnDestroy()
        {
            Debug.Log("ONDESTROY");
            if (hideTween != null)
                hideTween.Kill();
        }
    }
}