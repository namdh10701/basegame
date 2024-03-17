using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Slash.Unity.GestureInput.Raycasts
{
    public class PointerTouchDetection : IPointerTouchDetection
    {
        private readonly EventSystem eventSystem;

        private readonly List<RaycastResult> raycastResults = new List<RaycastResult>();

        private readonly PointerEventData pointerEventData;

        public PointerTouchDetection(EventSystem eventSystem)
        {
            this.eventSystem = eventSystem;
            this.pointerEventData = new PointerEventData(eventSystem);
        }

        public GameObject GetTouchedGameObject(Vector2 screenPosition)
        {
            this.pointerEventData.position = screenPosition;
            this.eventSystem.RaycastAll(this.pointerEventData, this.raycastResults);
            var raycastResult = FindFirstRaycast(this.raycastResults);
            return raycastResult.gameObject;
        }

        public IEnumerable<GameObject> GetTouchedGameObjects(Vector2 screenPosition)
        {
            this.pointerEventData.position = screenPosition;
            this.eventSystem.RaycastAll(this.pointerEventData, this.raycastResults);
            return this.raycastResults.Select(raycastResult => raycastResult.gameObject);
        }

        private static RaycastResult FindFirstRaycast(IEnumerable<RaycastResult> candidates)
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