using System;
using UnityEngine;

namespace _Base.Scripts.RPG
{
    public abstract class Shooter: IShooter
    {
        public abstract Vector3 AimDirection { get; }
    }
}