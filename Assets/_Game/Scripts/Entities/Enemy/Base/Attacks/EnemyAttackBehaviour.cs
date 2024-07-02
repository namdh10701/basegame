using System.Collections;
using UnityEngine;

namespace _Game.Scripts.Battle
{
    public abstract class EnemyAttackBehaviour : MonoBehaviour
    {        
        public abstract IEnumerator AttackSequence();
        public abstract void DoAttack();

    }
}