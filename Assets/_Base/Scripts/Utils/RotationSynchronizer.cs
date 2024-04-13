using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Base.Scripts.Utils
{
    public class RotationSynchronizer: MonoBehaviour
    {
        [SerializeField]
        private Transform source;

        [SerializeField]
        private Transform destination;

        private void Awake()
        {
            Assert.IsNotNull(source);
            Assert.IsNotNull(destination);
        }

        private void Update()
        {
            if (source.hasChanged)
            {
                destination.rotation = source.rotation;
            }
        }
    }
}