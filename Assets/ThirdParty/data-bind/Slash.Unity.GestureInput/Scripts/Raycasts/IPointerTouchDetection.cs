using System.Collections.Generic;
using UnityEngine;

namespace Slash.Unity.GestureInput.Raycasts
{
    public interface IPointerTouchDetection
    {
        /// <summary>
        ///     Returns the game object which is touched by a pointer at the specified screen position.
        /// </summary>
        /// <param name="screenPosition">Screen position to check.</param>
        /// <returns>Game Object touched by a pointer at the specified screen position.</returns>
        GameObject GetTouchedGameObject(Vector2 screenPosition);

        /// <summary>
        ///     Returns touched game objects in the order of being touched by a ray from the specified screen position.
        /// </summary>
        /// <param name="screenPosition">Screen position to check.</param>
        /// <returns>Touched game objects in the order of being touched by a ray.</returns>
        IEnumerable<GameObject> GetTouchedGameObjects(Vector2 screenPosition);
    }
}