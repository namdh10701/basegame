using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Slash.Unity.GestureInput.Raycasts
{
    /// <summary>
    ///     Raycaster which hits its own game object to use it for all events that fall through.
    /// </summary>
    public class FallbackRaycaster : BaseRaycaster
    {
        /// <summary>
        ///     Sorting layer to use for hit result.
        /// </summary>
        public int SortingLayer;

        /// <summary>
        ///     Sorting order, only relevant if there are multiple fallback raycasters.
        /// </summary>
        public int SortOrder = int.MinValue;

        /// <summary>
        ///     Fallback target to use for input.
        /// </summary>
        [Tooltip("Fallback target to use for input.")]
        public GameObject Target;

        public override Camera eventCamera
        {
            get
            {
                return null;
            }
        }

        public override int renderOrderPriority
        {
            get
            {
                return int.MinValue;
            }
        }

        public override int sortOrderPriority
        {
            get
            {
                return this.SortOrder;
            }
        }

        public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
        {
            resultAppendList.Add(
                new RaycastResult
                {
                    depth = int.MinValue,
                    distance = float.MaxValue,
                    gameObject = this.Target,
                    index = int.MaxValue,
                    module = this,
                    sortingLayer = this.SortingLayer,
                    sortingOrder = this.SortOrder
                });
        }
    }
}