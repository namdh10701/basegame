using UnityEngine;

namespace _Base.Scripts.Patterns.BuiltInPool
{
    public abstract class PoolObject : MonoBehaviour
    {
        protected PoolManager poolManager;
        public void SetPool(PoolManager poolManager)
        {
            this.poolManager = poolManager;
        }
        public abstract void OnReset();
    }
}
