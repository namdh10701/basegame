using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Slash.Unity.GestureInput.Devices.Implementations
{
    [Serializable]
    public class ReticlePointerDevice : IPointerDevice
    {
        public Camera Camera;

        private EventSystem eventSystem;

        private PointerEventData pointerEventData;

        public GameObject GetPointedGameObject()
        {
            return this.pointerEventData.pointerCurrentRaycast.gameObject;
        }

        public Vector2 GetPosition()
        {
            return this.pointerEventData.position;
        }

        public void Init(EventSystem eventSystem)
        {
            this.eventSystem = eventSystem;
            this.pointerEventData = new PointerEventData(this.eventSystem);
        }

        public bool IsDown()
        {
            return false;
        }

        public void Update()
        {
            this.pointerEventData.position = this.Camera != null
                ? (Vector2) this.Camera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f))
                : Vector2.zero;
            if (this.eventSystem != null)
            {
                var raycastResults = new List<RaycastResult>();
                this.eventSystem.RaycastAll(this.pointerEventData, raycastResults);
                this.pointerEventData.pointerCurrentRaycast = FindFirstRaycast(raycastResults);
            }
        }

        protected static RaycastResult FindFirstRaycast(List<RaycastResult> candidates)
        {
            foreach (var raycastResult in candidates)
            {
                if (raycastResult.gameObject == null)
                {
                    continue;
                }

                return raycastResult;
            }
            return new RaycastResult();
        }
    }
}