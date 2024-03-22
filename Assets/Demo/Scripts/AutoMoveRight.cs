using UnityEngine;

namespace Demo.Scripts
{
    public class AutoMoveRight : MonoBehaviour
    {
        Rigidbody2D body;
        [SerializeField] float speed;
        // Start is called before the first frame update
        void Start()
        {
            body = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            body.velocity = new Vector2(speed, 0);
        }
    }
}
