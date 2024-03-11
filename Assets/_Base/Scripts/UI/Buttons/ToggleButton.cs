using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Core.UI
{
    public class ToggleButton : MonoBehaviour
    {
        public enum ToggleState
        {
            ON, OFF
        }
        [SerializeField] Button _button;
        private ToggleState _currentState;

        public UnityEvent _onAction;
        public UnityEvent _offAction;

        public Transform onPos;
        public Transform offPos;
        public CanvasGroup canvasGroup;
        public Transform circle;

        private void Awake()
        {
            _button.onClick.AddListener(
                () => Toggle()
            );
        }

        public void Init(ToggleState state)
        {
            _currentState = state;
            ToggleInit(state);
        }


        public void Init(UnityEvent onAction, UnityEvent offAction)
        {
            _onAction = onAction;
            _offAction = offAction;
        }

        public void Toggle(ToggleState state)
        {
            _currentState = state;
            if (state == ToggleState.ON)
            {
                _onAction.Invoke();
                circle.DOLocalMove(onPos.localPosition, .3f);
                canvasGroup.DOFade(1, .3f);
            }
            else
            {
                _offAction.Invoke();
                circle.DOLocalMove(offPos.localPosition, .3f);
                canvasGroup.DOFade(0, .3f);
            }


        }
        public void ToggleInit(ToggleState state)
        {
            _currentState = state;
            if (state == ToggleState.ON)
            {
                circle.localPosition = onPos.localPosition;
                canvasGroup.alpha = 1;
            }
            else
            {
                circle.localPosition = offPos.localPosition;
                canvasGroup.alpha = 0;
            }


        }
        public void Toggle()
        {
            Toggle(_currentState == ToggleState.ON ? ToggleState.OFF : ToggleState.ON);
        }
    }
}
