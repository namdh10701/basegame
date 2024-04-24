namespace _Base.Scripts.RPGCommon.Behaviours.AttackStrategies
{
    public class ManualShootTargetTriggerBehaviour : ShootTargetTriggerBehaviour
    {
        public override void Pull()
        {
            if (Ammo != null)
            {
                if (Ammo.Value <= Ammo.MinValue)
                    return;
                Ammo.StatValue.BaseValue--;
            }
            AttackTargetBehaviour.Attack();
        }

        public override void Release()
        {

        }
    }
}