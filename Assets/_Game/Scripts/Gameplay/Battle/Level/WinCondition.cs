
using System;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public abstract class WinCondition : MonoBehaviour
    {
        protected bool IsChecking;
        public virtual void StartChecking()
        {
            IsChecking = true;
        }

        public virtual void StopCheck()
        {
            IsChecking = false;
        }
    }
}