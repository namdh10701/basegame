using UnityEngine;

namespace _Base.Scripts.Patterns.BuiltInPool
{
    public abstract class PoolManager : MonoBehaviour
    {
        UnityEngine.Pool.ObjectPool<PoolObject> pool;
        [SerializeField] int defaultCapacity;
        [SerializeField] int maxSize;
        [SerializeField] protected PoolObject poolObjectPrefab;
        private void Start()
        {
            pool = new UnityEngine.Pool.ObjectPool<PoolObject>(CreateObject, GetObject, ReleaseObject, ActionOnDestroy, true, defaultCapacity, maxSize);
        }

        protected abstract PoolObject CreateObject();
        protected abstract void GetObject(PoolObject getObject);
        protected virtual void ReleaseObject(PoolObject releaseObject)
        {
            releaseObject.OnRelease();
        }
        protected abstract void ActionOnDestroy(PoolObject onDestroyObject);

        public virtual PoolObject Pull()
        {
            return pool.Get();
        }

        public virtual void Release(PoolObject poolObject)
        {
            pool.Release(poolObject);
        }
    }
}
