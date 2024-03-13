using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigerer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Command"))
        {
            if (collision.TryGetComponent(out ITriggerEnterEvent command))
            {
                command.Execute();
            }
        }
    }
}
