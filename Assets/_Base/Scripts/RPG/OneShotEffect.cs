using System;
using UnityEngine;

namespace _Base.Scripts.RPG
{
    public abstract class OneShotEffect: Effect
    {
        private void Start()
        {
            DoStart();
            Apply();
            DoEnd();
            Destroy(this);
        }
    }
}