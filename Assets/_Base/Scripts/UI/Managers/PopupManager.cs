using System.Collections.Generic;
using System.Linq;
using _Base.Scripts.Utils;
using UnityEngine;

namespace _Base.Scripts.UI.Managers
{
    public class PopupManager : SingletonMonoBehaviour<PopupManager>
    {
        List<Popup> popups;
        Popup currentPopup;
        Popup prevPopup;
        readonly Stack<Popup> activePopupLayers = new();
        protected override void Awake()
        {
            base.Awake();
            popups = FindObjectsByType<Popup>(FindObjectsInactive.Include,FindObjectsSortMode.None).ToList();
            foreach(Popup popup in popups)
            {
                popup.HideImmediately();
            }
        }
        public void HidePopup(Popup popup)
        {
            popup.Hide();
        }

        public void ShowPopup<T>()
        {
            foreach (var view in popups)
            {
                if (view is T)
                {
                    ShowPopup(view);
                    break;
                }
            }
        }
        public void ShowPopup(Popup popup)
        {
            prevPopup = currentPopup;
            currentPopup = popup;
            popup.Show();
            activePopupLayers.Push(popup);
        }

    }
}