using System;
using UnityEngine;

namespace _Base.Scripts.RPG
{
    public interface IShooter
    {
        public Vector3 AimDirection { get; }
    }
}