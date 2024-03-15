using System;
using UnityEngine;

namespace _Base.Scripts.World
{
    public abstract class OneShotEffect: Effect
    {

        private void Update()
        {
            Apply();
            Destroy(this);
        }
    }
}