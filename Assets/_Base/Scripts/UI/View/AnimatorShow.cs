using UnityEngine;

namespace _Base.Scripts.UI.Viewx
{
    public class AnimatorShow : MonoBehaviour, ICommand
    {
        View view;
        private void Awake()
        {
            view = GetComponent<View>();
        }
        public void Execute()
        {
            view.animator.Play("Show");
            view.ViewState = ViewState.Showing;
            view.onShowStart?.Invoke();
        }

        public void Interrupt()
        {
        }

        public void OnCompleted()
        {
            view.ViewState = ViewState.Show;
        }
        public void OnShowCompleted()
        {
            Debug.Log("AAAShow");
            view.onShowEnd?.Invoke();
        }
    }
}