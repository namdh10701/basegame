using UnityEngine;

namespace _Base.Scripts.RPGCommon.Entities
{
    public abstract class Shooter: IShooter
    {
        public abstract Vector3 AimDirection { get; }
    }
}