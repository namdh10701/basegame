namespace _Base.Scripts.RPGCommon.Behaviours.AttackStrategies
{
    public class AutoShootTargetTriggerBehaviour : ManualShootTargetTriggerBehaviour
    {

        private void Awake()
        {
            if (fireRate != null)
            {
                InvokeRepeating("Pull", 0f,
                fireRate.Value);
            }
            else
            {
                InvokeRepeating("Pull", 0, 1);
            }
            // Pull();
        }
    }
}