using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    public abstract class Condition : MonoBehaviour
    {
        [SerializeField] protected bool isMet;
        [SerializeField] protected List<Condition> precedeConditions = new List<Condition>();
        public bool IsMet
        {
            get
            {
                foreach (Condition condition in precedeConditions)
                {
                    isMet &= condition.isMet;
                }
                return isMet;
            }
        }
    }
}