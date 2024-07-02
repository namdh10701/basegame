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