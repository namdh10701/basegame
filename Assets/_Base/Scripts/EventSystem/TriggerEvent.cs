using UnityEngine;

namespace _Base.Scripts.EventSystem
{
    /// <summary>
    /// Raises an event on trigger collision
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class TriggerEvent : MonoBehaviour
    {
        const string k_PlayerTag = "Player";

        [SerializeField]
        BaseEvent m_Event;

        void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag(k_PlayerTag))
            {
                if (m_Event != null)
                {
                    m_Event.Raise();
                }
            }
        }
    }
}
