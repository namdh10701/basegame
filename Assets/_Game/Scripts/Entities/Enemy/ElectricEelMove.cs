using MBT;
using UnityEngine;
namespace _Game.Scripts.Entities
{
    [MBTNode("ElectricEel/Move")]
    [AddComponentMenu("")]
    public class ElectricEelMove : Leaf
    {
        public Rigidbody2D body;
        public float moveSpeed = 5f;
        public float waveFrequency = 2f; // Tần số sóng
        public float waveAmplitude = 1f; // Biên độ sóng
        public float dashSpeed = 10f; // Tốc độ khi dash
        public float dashDuration = 0.2f; // Thời gian dash
        public float dashCooldown = 2f; // Thời gian hồi chiêu của dash
        public float changeDirectionInterval = 3f; // Thời gian giữa các lần đổi hướng

        public BoxCollider2D movementBounds; // Box Collider để giới hạn di chuyển

        private Vector2 direction;
        private float dashTimer;
        private float dashCooldownTimer;
        private bool isDashing = false;
        private float changeDirectionTimer;

        void Start()
        {
            direction = GetRandomDirection();
            changeDirectionTimer = changeDirectionInterval;
        }

        void Move()
        {
            float waveOffset = Mathf.Sin(Time.time * waveFrequency) * waveAmplitude;
            Vector2 waveMovement = new Vector2(direction.x, waveOffset).normalized;
            body.velocity = waveMovement * moveSpeed;
        }

        void StartDash()
        {
            isDashing = true;
            dashTimer = dashDuration;
            dashCooldownTimer = dashCooldown;
            body.velocity = direction * dashSpeed;
        }

        void Dash()
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                isDashing = false;
                body.velocity = direction * moveSpeed;
            }
        }

        void HandleDirectionChange()
        {
            if (changeDirectionTimer <= 0)
            {
                direction = GetRandomDirection();
                changeDirectionTimer = changeDirectionInterval;
            }
        }

        Vector2 GetRandomDirection()
        {
            float angle = Random.Range(0f, 360f);
            return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
        }

        void ClampToBounds()
        {
            Vector3 clampedPosition = transform.position;
            Bounds bounds = movementBounds.bounds;

            clampedPosition.x = Mathf.Clamp(clampedPosition.x, bounds.min.x, bounds.max.x);
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, bounds.min.y, bounds.max.y);

            transform.position = clampedPosition;
        }

        public override NodeResult Execute()
        {
            if (!isDashing && dashCooldownTimer <= 0 && Random.value < 0.01f)
            {
                StartDash();
            }

            if (isDashing)
            {
                Dash();
            }
            else
            {
                Move();
                HandleDirectionChange();
            }

            dashCooldownTimer -= Time.deltaTime;
            changeDirectionTimer -= Time.deltaTime;

            // Ensure the eel stays within the bounds
            ClampToBounds();
            return NodeResult.success;
        }
    }
}

