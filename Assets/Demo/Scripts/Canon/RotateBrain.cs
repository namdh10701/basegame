using UnityEngine;

namespace Demo.Scripts.Canon
{
    public class RotateBrain : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed;
        [SerializeField] Transform _transform;
        Quaternion restRotation;

        [HideInInspector] public bool IsLookingAtTarget;

        private void Start()
        {
            restRotation = _transform.rotation;
        }
        public void Rotate(Transform target)
        {
            Vector3 targetDirection = target.position - _transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, targetDirection);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            float angle = Quaternion.Angle(_transform.rotation, targetRotation);
            IsLookingAtTarget = angle < 2f;
        }

        internal void ResetRotate()
        {
            _transform.rotation = Quaternion.Slerp(_transform.rotation, restRotation, rotateSpeed * Time.deltaTime);
            IsLookingAtTarget = false;
        }
    }
}