using _Base.Scripts.RPG.Effects;
using _Game.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace _Game.Scripts.Battle
{
    public abstract class EnemyAttackBehaviour : MonoBehaviour
    {        
        public abstract IEnumerator AttackSequence();
        public abstract void DoAttack();

    }
}