using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// A singleton that manages display state and access to UI Views 
/// </summary>
public class ViewManager : AbstractSingleton<ViewManager>
{
    List<View> views;
    View currentView;
    readonly Stack<View> m_History = new();

    protected override void Awake()
    {
        base.Awake();
        views = FindObjectsByType<View>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
        Init();
    }

    void Init()
    {
        foreach (var view in views)
            view.Deactive();
        m_History.Clear();

    }
    public T GetView<T>() where T : View
    {
        foreach (var view in views)
        {
            if (view is T tView)
            {
                return tView;
            }
        }
        return null;
    }
    public void Show<T>(bool keepInHistory = true) where T : View
    {
        foreach (var view in views)
        {
            if (view is T)
            {
                Show(view, keepInHistory);
                break;
            }
        }
    }
    public void Show<T>(Transition transition, bool keepInHistory = true) where T : View
    {
        foreach (var view in views)
        {
            if (view is T)
            {
                switch (transition)
                {
                    case Transition.None:
                        {
                            Show(view, keepInHistory);
                        }
                        break;
                    case Transition.CrossFade:
                        {
                            ViewTransitionManager.Instance.TransitCrossFade(() => Show(view, keepInHistory));
                        }
                        break;
                }
                break;
            }
        }
    }
    public void Show(View view, bool keepInHistory)
    {
        if (currentView != null)
        {
            if (keepInHistory)
            {
                m_History.Push(currentView);
            }
            Hide(currentView, () => view.Show());
            currentView = view;
        }
        else
        {
            view.Show();
            currentView = view;
        }
    }

    public void Hide(View view, Action onHideCompleted)
    {
        view.Hide(onHideCompleted);
    }

    public void Show(View view, Transition transition = Transition.None, bool keepInHistory = true)
    {
        switch (transition)
        {
            case Transition.None:
                Show(view, keepInHistory);
                break;
            case Transition.CrossFade:
                {
                    ViewTransitionManager.Instance.TransitCrossFade(() => Show(view, keepInHistory));
                }
                break;
        }
    }

    public void GoBack()
    {
        if (m_History.Count != 0)
        {
            Show(m_History.Pop(), false);
        }
    }
}
