using UnityEngine;

namespace _Game.Features.Common
{
    public class GameObjectActiveController : MonoBehaviour
    {
        public void SetActiveInverse(bool active)
        {
            gameObject.SetActive(!active);
        }
    }
}