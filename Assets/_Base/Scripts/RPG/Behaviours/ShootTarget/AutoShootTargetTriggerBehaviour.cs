namespace _Base.Scripts.RPG.Behaviours.ShootTarget
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