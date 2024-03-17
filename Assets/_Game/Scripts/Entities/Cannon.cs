using _Base.Scripts.RPG;
using UnityEngine;

namespace _Game.Scripts.Entities
{
    public class Cannon: Entity, IShooter
    {
        public Vector3 AimDirection => transform.rotation.eulerAngles;
    }
}