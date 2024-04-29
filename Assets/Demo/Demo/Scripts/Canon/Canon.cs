using Demo.ScriptableObjects.Scripts;
using DG.Tweening;
using UnityEngine;

namespace Demo.Scripts.Canon
{
    public class Canon : MonoBehaviour
    {
        [SerializeField] private RotateBrain rotateBrain;
        [SerializeField] private SightBrain sightBrain;
        [SerializeField] private CooldownBrain cooldownBrain;
        [SerializeField] public Transform Visual;
        [SerializeField] SpriteRenderer mainvisual;
        [SerializeField] public Transform AttackTrigger;
        Sequence outOfBulletSequence;
        Color orgColor;
        private void Start()
        {
            orgColor = mainvisual.color;
            outOfBulletSequence = DOTween.Sequence();
            outOfBulletSequence.Append(mainvisual.DOFade(.65f, .3f));
            outOfBulletSequence.Append(mainvisual.DOFade(1f, .3f));
            outOfBulletSequence.SetAutoKill(false);
            outOfBulletSequence.SetLoops(-1);
            outOfBulletSequence.Pause();
        }
        public enum State
        {
            Idle, Rotating, Attacking
        }

        private State currentState = State.Idle;

        void UpdateState()
        {
            switch (currentState)
            {
                case State.Idle:
                    if (sightBrain.CurrentTarget != null)
                    {
                        currentState = State.Rotating;
                    }
                    break;
                case State.Rotating:
                    if (rotateBrain.IsLookingAtTarget)
                    {
                        currentState = State.Attacking;
                    }
                    if (sightBrain.CurrentTarget == null)
                    {
                        currentState = State.Idle;
                    }
                    break;
                case State.Attacking:
                    if (sightBrain.CurrentTarget == null)
                    {
                        currentState = State.Idle;
                    }
                    break;
            }
        }
        private void Update()
        {

            UpdateState();
            sightBrain.FindTarget();
            switch (currentState)
            {
                case State.Idle:
                    rotateBrain.ResetRotate();
                    break;
                case State.Rotating:
                    rotateBrain.Rotate(sightBrain.CurrentTarget.transform);

                    if (cooldownBrain.IsResting)
                    {
                        cooldownBrain.WarmUp();
                    }
                    break;
                case State.Attacking:
                    rotateBrain.Rotate(sightBrain.CurrentTarget.transform);

                    if (cooldownBrain.IsResting)
                    {
                        cooldownBrain.WarmUp();
                    }

                    if (!cooldownBrain.IsInCooldown && !cooldownBrain.IsResting)
                    {
                        if (cbm.CurrentBullet > 0)
                        {
                            cbm.OnShoot();
                            cooldownBrain.StartCooldown();
                        }
                        else
                        {
                            if (!outOfBulletSequence.IsPlaying())
                                outOfBulletSequence.Play();
                        }
                    }
                    break;
            }

        }

        public void Reload()
        {
            outOfBulletSequence.Pause();
            mainvisual.color = orgColor;
            cbm.OnReload();
        }

        [SerializeField] CannonBulletManager cbm;
        // nếu cooldown xong đợi 0.5s mà không gọi cooldown tiếp thì đi vào trạng thái rest, lần sau sẽ cần đợi warm up trước khi bắn phát đầu
    }
}