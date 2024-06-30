using _Game.Scripts;
using UnityEngine;
namespace _Base.Scripts.RPGCommon.Behaviours.AttackStrategies
{
    public class AutoShootTargetTriggerBehaviour : ManualShootTargetTriggerBehaviour
    {
        public float timer;
        protected override void Awake()
        {
            base.Awake();
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (!AimTargetBehaviour.IsReadyToAttack)
            {
                timer = 0;
            }
            if (timer > (1 / fireRate.Value) && AimTargetBehaviour.IsReadyToAttack)
            {
                Pull();
                timer = 0;
            }

        }
    }
}