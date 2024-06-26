using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace _Game.Common
{
    public class PointerTracker : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerClickHandler
    {
        [Tooltip("Time before the long press triggered")]
        public float holdTime = 0.001f;

        [Tooltip("long press event trigger interval")]
        public float intervalTime = 0.001f;

        public UnityEvent onDown = new ();
        public UnityEvent onUp = new ();
        public UnityEvent onClick = new ();

        public UnityEvent onLongPress = new ();

        public void OnPointerDown(PointerEventData eventData)
        {
            onDown.Invoke();
            InvokeRepeating("OnLongPress", holdTime, intervalTime);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            onUp.Invoke();
            //Debug.Log("Stop Long Pressing");
            CancelInvoke("OnLongPress");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            //Debug.Log("Stop Long Pressing");
            CancelInvoke("OnLongPress");
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            onClick.Invoke();
        }

        private void OnLongPress()
        {
            try
            {
                //Debug.Log("Long Press is ongoing");
                onLongPress.Invoke();
            }
            catch (Exception ex)
            {
                Debug.Log(ex.ToString());
            }
        }
    }
}