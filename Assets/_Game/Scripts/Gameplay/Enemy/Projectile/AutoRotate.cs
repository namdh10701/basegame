
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    public float rotateSpeed;
    private void Update()
    {
        transform.rotation *= Quaternion.Euler(0, 0, rotateSpeed * Time.deltaTime);
    }
}
