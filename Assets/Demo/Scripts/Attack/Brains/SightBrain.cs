using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class SightBrain : MonoBehaviour
{

    //TODO : delay lock target
    public enum LockingTargetStrategy
    {
        FurthestPersit,
        FurthestFlex,
        ClosetPersit,
        ClosetFlex,
        HighestHp,
        HighestDmg
    }
    [SerializeField] LockingTargetStrategy strategy;
    List<DefenseBehaviour> defenseBehaviours = new List<DefenseBehaviour>();
    private DefenseBehaviour currentTarget;
    public DefenseBehaviour CurrentTarget => currentTarget;
    public string targetLayerName;

    private int targetLayer;

    private void Start()
    {
        targetLayer = LayerMask.NameToLayer(targetLayerName);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == targetLayer)
        {
            DefenseBehaviour defenseBehaviour;

            collision.TryGetComponent<DefenseBehaviour>(out defenseBehaviour);
            if (defenseBehaviour != null)
            {
                if (!defenseBehaviours.Contains(defenseBehaviour))
                    defenseBehaviours.Add(defenseBehaviour);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == targetLayer)
        {
            DefenseBehaviour defenseBehaviour;

            collision.TryGetComponent<DefenseBehaviour>(out defenseBehaviour);
            if (defenseBehaviour != null)
            {
                if (defenseBehaviours.Contains(defenseBehaviour))
                    defenseBehaviours.Remove(defenseBehaviour);

                if (CurrentTarget == defenseBehaviour)
                {
                    currentTarget = null;
                }
            }
        }
    }


    public void FindTarget()
    {
        if (CurrentTarget == null)
        {
            currentTarget = GetClosetTarget();
        }
    }

    DefenseBehaviour GetClosetTarget()
    {
        DefenseBehaviour closestTarget = null;
        float closestDistance = Mathf.Infinity;
        Vector2 currentPosition = transform.position;

        foreach (DefenseBehaviour defenseBehaviour in defenseBehaviours)
        {
            Vector2 targetPosition = defenseBehaviour.transform.position;
            float distanceToTarget = Vector2.Distance(currentPosition, targetPosition);
            if (distanceToTarget < closestDistance)
            {
                closestDistance = distanceToTarget;
                closestTarget = defenseBehaviour;
            }
        }

        return closestTarget;
    }
}
