using System;
using System.Collections.Generic;
using System.Linq;
using _Base.Scripts.Utils;
using _Base.Scripts.UI;
using UnityEngine;
using _Base.Scripts.Shared;
using System.Collections;
using UnityEngine.SceneManagement;
using _Game.Scripts.UI;

namespace _Base.Scripts.UI.Managers
{
    /// <summary>
    /// A singleton that manages display state and access to UI Views 
    /// </summary>
    public class ViewManager : MonoBehaviour
    {
        Dictionary<Type, string> prefabPaths = new Dictionary<Type, string>
        {
            { typeof(View), "Prefabs/UI/SettingPopup" },
           
        };

        private static ViewManager instance;
        public static ViewManager Instance => instance;
        List<View> views;
        View currentView;
        readonly Stack<View> m_History = new();

        private void Awake()
        {
            instance = this;
            views = FindObjectsByType<View>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
            Init();
        }

        void Init()
        {
            foreach (var view in views)
            {
                view.CheckInitialize();
                view.Deactive();
            }
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
            return CreateView<T>();
        }
        public void Show<T>(bool keepInHistory = true) where T : View
        {
            foreach (var view in views)
            {
                if (view is T)
                {
                    Show(view, keepInHistory);
                    return;
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
                    return;
                }
            }
            T createdView = CreateView<T>();
            Show(createdView, transition, keepInHistory);
        }

        T CreateView<T>() where T : View
        {
            T viewPrefab = Resources.Load<T>(prefabPaths[typeof(T)]);
            T view = Instantiate(viewPrefab, transform);
            views.Add(view);
            return view;
        }

        public void Show(View view, bool keepInHistory)
        {
            if (currentView != null)
            {
                if (keepInHistory)
                {
                    m_History.Push(currentView);
                }
                Hide(currentView, null);
                view.Show();
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
}
