using UnityEngine;

namespace _Game.Features.Inventory
{
    public abstract class DragDataProvider: MonoBehaviour
    {
        public abstract T GetData<T>() where T : class;
    }
}