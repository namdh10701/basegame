using UnityEngine;
public class AnimatorShow : MonoBehaviour, Command
{
    View view;
    private void Awake()
    {
        view = GetComponent<View>();
    }
    public void Execute()
    {
        view.animator.Play("Show");
        view.ViewState = ViewState.SHOWING;
        view.onShowStart?.Invoke();
    }

    public void Interupt()
    {
    }

    public void OnCompleted()
    {
        view.ViewState = ViewState.SHOW;
    }
    public void OnShowCompleted()
    {
        Debug.Log("AAAShow");
        view.onShowEnd?.Invoke();
    }
}