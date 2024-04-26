using _Base.Scripts.RPG.Stats;
using _Game.Scripts;

namespace _Base.Scripts.RPGCommon.Behaviours.AttackStrategies
{
    public class ManualShootTargetTriggerBehaviour : ShootTargetTriggerBehaviour
    {
        public override void Pull()
        {
            
            AttackTargetBehaviour.Attack();
        }

        public override void Release()
        {

        }
    }
}