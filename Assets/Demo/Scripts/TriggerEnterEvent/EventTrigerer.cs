using UnityEngine;

namespace Demo.Scripts.TriggerEnterEvent
{
    public class EventTrigerer : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Command"))
            {
                ITriggerEnterEvent[] events = collision.GetComponents<ITriggerEnterEvent>();
                foreach (ITriggerEnterEvent IEvent in events)
                {
                    IEvent.Execute();
                }
            }
        }
    }
}
