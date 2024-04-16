using UnityEngine;

namespace _Base.Scripts.UI.Viewx
{
    public class AnimatorShow : MonoBehaviour, ICommand
    {
        View view;
        bool initialized;
        private void Awake()
        {
            Initialize();

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

        public void Initialize()
        {
            if (initialized)
                return;
            initialized = true;
            view = GetComponent<View>();
        }
    }
}