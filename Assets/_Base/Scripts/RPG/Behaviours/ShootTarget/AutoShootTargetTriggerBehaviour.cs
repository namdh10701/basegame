namespace _Base.Scripts.RPG.Behaviours.ShootTarget
{
    public class AutoShootTargetTriggerBehaviour: ManualShootTargetTriggerBehaviour
    {

        private void Awake()
        {
            Pull();
        }
    }
}