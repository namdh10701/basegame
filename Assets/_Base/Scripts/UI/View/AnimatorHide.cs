using UnityEngine;

namespace _Base.Scripts.UI.Viewx
{
    public class AnimatorHide : MonoBehaviour, Command
    {
        View view;
        private void Awake()
        {
            view = GetComponent<View>();
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
    }
}