using UnityEngine;

public class MoveBehaviour : MonoBehaviour
{
    [SerializeField] Rigidbody2D body;
    [SerializeField] private float frequency;
    [SerializeField] private float amplitude;
    [SerializeField] private float modifier;
    public void Move()
    {
        body.velocity = Mathf.Clamp((Mathf.Cos(Time.time * frequency) * amplitude + modifier), 0, 10000) * Vector2.down;
    }
    public void StopMove()
    {
        body.velocity = Vector2.zero;
    }
}
