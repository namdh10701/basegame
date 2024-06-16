using _Game.Scripts;
using UnityEngine;
namespace _Base.Scripts.RPGCommon.Behaviours.AttackStrategies
{
    public class AutoShootTargetTriggerBehaviour : ManualShootTargetTriggerBehaviour
    {
        float timer;
        protected override void Awake()
        {
            base.Awake();
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer > fireRate.Value)
            {
                Pull();
                timer = 0;
            }
        }
    }
}