using _Base.Scripts.RPG;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Entities;
using UnityEngine;

namespace _Game.Scripts.Entities
{
    public class Cannon: Entity, IShooter
    {
        public Vector3 AimDirection => transform.rotation.eulerAngles;
    }
}