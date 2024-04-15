using System;
using System.Collections.Generic;
using System.Linq;
using _Base.Scripts.Utils;
using _Game.Scripts.UI;
using UnityEngine;

namespace _Base.Scripts.UI.Managers
{
    public class PopupManager : MonoBehaviour
    {
        private static PopupManager instance;
        public static PopupManager Instance => instance;


        Dictionary<Type, string> prefabPaths = new Dictionary<Type, string>
    {
        { typeof(SettingPopup), "Prefabs/UI/SettingPopup" },
        { typeof(Popup), "Prefabs/UI/Popup" }
    };
        List<Popup> popups;
        Popup currentPopup;
        Popup prevPopup;
        readonly Stack<Popup> activePopupLayers = new();

        [SerializeField] Transform popupRoot;
        private void Awake()
        {
            instance = this;
            popups = FindObjectsByType<Popup>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
            foreach (Popup popup in popups)
            {
                popup.HideImmediately();
            }
        }
        public void HidePopup(Popup popup)
        {
            popup.Hide();
        }

        public void ShowPopup<T>() where T : Popup
        {
            foreach (var view in popups)
            {
                if (view is T)
                {
                    ShowPopup(view);
                    return;
                }
            }
            T popup = CreatePopup<T>();
            popup.Show();
        }
        public void ShowPopup(Popup popup)
        {
            prevPopup = currentPopup;
            currentPopup = popup;
            popup.Show();
            activePopupLayers.Push(popup);
        }
        public T GetPopup<T>() where T : Popup
        {
            foreach (var view in popups)
            {
                if (view is T)
                {
                    return (T)view;
                }
            }
            return CreatePopup<T>();
        }

        private T CreatePopup<T>() where T : Popup
        {
            string prefabPath;
            prefabPath = prefabPaths[typeof(T)];
            T popupPrefab = Resources.Load<T>(prefabPath);
            T popup = Instantiate(popupPrefab, popupRoot);
            popups.Add(popup);
            return popup;
        }
    }
}