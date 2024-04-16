using UnityEngine;

namespace _Base.Scripts.UI.Viewx
{
    public class AnimatorHide : MonoBehaviour, ICommand
    {
        View view;
        bool initialized;
        private void Awake()
        {
            Initialize();
        }
        public void Execute()
        {
            view.animator.Play("Hide");
            view.ViewState = ViewState.Hiding;
            view.onHideStart?.Invoke();
        }

        public void Interrupt()
        {
        }

        public void OnCompleted()
        {
            view.ViewState = ViewState.Hide;
        }
        public void OnHideCompleted()
        {
            Debug.Log("AAAHIDE");
            view.onHideEnd?.Invoke();
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