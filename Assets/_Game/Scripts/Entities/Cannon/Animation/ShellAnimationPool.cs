using _Base.Scripts.Patterns.BuiltInPool;
using UnityEngine;

namespace _Game.Scripts
{
    public class ShellAnimationPool : PoolManager
    {
        public Transform root;
        protected override void ActionOnDestroy(PoolObject onDestroyObject)
        {
            GameObject.Destroy(onDestroyObject);
        }

        protected override PoolObject CreateObject()
        {
            ShellAnimation shellAnim = Instantiate(poolObjectPrefab, root.transform.position, Quaternion.identity, root).GetComponent<ShellAnimation>();
            shellAnim.SetPool(this);
            return shellAnim;
        }

        protected override void GetObject(PoolObject getObject)
        {
            getObject.gameObject.SetActive(true);
        }
    }
}
