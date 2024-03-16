using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Slash.Unity.GestureInput.Sources
{
    public class ClickSource : MonoBehaviour, IPointerClickHandler, IPointerDownHandler
    {
        public ClickEvent Click = new ClickEvent();

        public void OnPointerClick(PointerEventData eventData)
        {
            this.Click.Invoke(
                new ClickEventData
                {
                    GameObject = this.gameObject,
                    ScreenPosition = eventData.position,
                    WorldPosition = eventData.pointerCurrentRaycast.worldPosition
                });
        }

        public void OnPointerDown(PointerEventData eventData)
        {
        }

        public class ClickEventData
        {
            /// <summary>
            ///     Clicked game object.
            /// </summary>
            public GameObject GameObject { get; set; }

            /// <summary>
            ///     Screen position of click.
            /// </summary>
            public Vector2 ScreenPosition { get; set; }

            /// <summary>
            ///     World position of click.
            /// </summary>
            public Vector3 WorldPosition { get; set; }
        }

        [Serializable]
        public class ClickEvent : UnityEvent<ClickEventData>
        {
        }
    }
}