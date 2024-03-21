namespace _Base.Scripts.RPGCommon.Behaviours.AttackStrategies
{
    public class ManualShootTargetTriggerBehaviour: ShootTargetTriggerBehaviour
    {
        public override void Pull()
        {
            shootTargetBehaviour.Attack();
        }

        public override void Release()
        {
            
        }
    }
}