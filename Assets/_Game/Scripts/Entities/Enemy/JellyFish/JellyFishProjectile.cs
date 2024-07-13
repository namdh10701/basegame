using _Game.Scripts;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class JellyFishProjectile : MonoBehaviour
    {
        [SerializeField] Rigidbody2D body;
        EnemyAttackData atkData;
        public float speed;
        public float rotateSpeed;
        [SerializeField] ParticleSystem particle;
        [SerializeField] ParticleSystem trail;
        public bool isLaunched;
        public void SetData(EnemyAttackData atkData, Vector2 startPos, float deviation)
        {
            this.atkData = atkData;
            transform.position = startPos;
            var targetPosition = atkData.CenterCell.transform.position;
            Vector2 targetDirection = (targetPosition - transform.position).normalized;
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            Quaternion newRotation = Quaternion.AngleAxis(angle - 90 + deviation, Vector3.forward);
            transform.rotation = newRotation;
        }

        private void FixedUpdate()
        {

            if (isLaunched && atkData != null && atkData.CenterCell != null)
            {
                Vector2 targetDirection = (atkData.CenterCell.transform.position - transform.position).normalized;
                float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

                float angleDifference = Quaternion.Angle(transform.rotation, targetRotation);

                if (angleDifference < rotateSpeed * Time.fixedDeltaTime)
                {
                    transform.rotation = targetRotation;
                }
                else
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * rotateSpeed);
                }

                body.velocity = speed * transform.up;

                if (Vector2.Distance(body.position, atkData.CenterCell.transform.position) < 0.5f)
                {
                    OnImpact();
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, transform.position + transform.up * 3);
        }
        public void Launch()
        {
            isLaunched = true;
        }
        void OnImpact()
        {
            trail.transform.parent = null;
            Destroy(gameObject);
            Instantiate(particle, atkData.CenterCell.transform.position, Quaternion.identity);
            GridAttackHandler attackHandler = FindAnyObjectByType<GridAttackHandler>();
            attackHandler.ProcessAttack(atkData);
        }
    }
}

