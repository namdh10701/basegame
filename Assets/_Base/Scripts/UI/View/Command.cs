using DG.Tweening;
using UnityEngine;

public enum ViewState
{
    SHOWING, SHOW, HIDING, HIDE
}
public interface Command
{
    public void Execute();
    public void Interupt();
    public void OnCompleted();
}