
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public abstract class WinCondition : MonoBehaviour
    {
        protected bool IsStarted;
        public virtual void StartChecking()
        {
            IsStarted = true;
        }
    }
}