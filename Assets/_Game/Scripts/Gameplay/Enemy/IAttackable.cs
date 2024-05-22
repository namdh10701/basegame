using System.Collections;

namespace _Game.Scripts
{
    public interface IAttackable
    {
        public IEnumerator AttackSequence();
    }
}