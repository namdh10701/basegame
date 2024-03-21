namespace _Base.Scripts.RPGCommon.Behaviours.AttackStrategies
{
    public class AutoShootTargetTriggerBehaviour: ManualShootTargetTriggerBehaviour
    {

        private void Awake()
        {
            InvokeRepeating("Pull", 0f, 1f);
            // Pull();
        }
    }
}