using UnityEngine;
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
        view.ViewState = ViewState.HIDING; 
        view.onHideStart?.Invoke();
    }

    public void Interupt()
    {
    }

    public void OnCompleted()
    {
        view.ViewState = ViewState.HIDE;
    }
    public void OnHideCompleted()
    {
        Debug.Log("AAAHIDE");
        view.onHideEnd?.Invoke();
    }
}