using System.Collections.Generic;
using UnityEngine;

namespace _Base.Scripts.World
{
    [DisallowMultipleComponent]
    public abstract class Entity: MonoBehaviour
    {
        public List<IAttribute> Attributes { get; set; }
        private void Update()
        {
            
        }
    }
}