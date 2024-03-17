namespace _Base.Scripts.RPG.Behaviours.ShootTarget
{
    public class ManualShootTargetTriggerBehaviour: ShootTargetTriggerBehaviour
    {
        public override void Pull()
        {
            shootTargetBehaviour.Shoot();
        }

        public override void Release()
        {
            throw new System.NotImplementedException();
        }
    }
}