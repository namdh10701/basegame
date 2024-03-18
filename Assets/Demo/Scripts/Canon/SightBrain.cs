using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using _Base.Scripts.Shared;

[RequireComponent(typeof(Collider2D))]
public class SightBrain : MonoBehaviour
{

    //TODO : delay lock target
    public enum LockingTargetStrategy
    {
        /*FurthestPersit,
        FurthestFlex,
        ClosetPersit,*/
        ClosetFlex,
        /*HighestHp,
        HighestDmg,*/
        LowestHp
    }
    [SerializeField] LockingTargetStrategy strategy;
    List<DefenseBehaviour> defenseBehaviours = new List<DefenseBehaviour>();
    private DefenseBehaviour currentTarget;
    public DefenseBehaviour CurrentTarget => currentTarget;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == GlobalData.EnemyLayer)
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
        if (collision.gameObject.layer == GlobalData.EnemyLayer)
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
        switch (strategy)
        {
            /*case LockingTargetStrategy.FurthestPersit:
                break;
            case LockingTargetStrategy.FurthestFlex:
                break;
            case LockingTargetStrategy.ClosetPersit:
                break;*/
            case LockingTargetStrategy.ClosetFlex:
                if (currentTarget == null)
                    currentTarget = GetClosetTarget();
                break;
            /* case LockingTargetStrategy.HighestHp:
                 break;
             case LockingTargetStrategy.HighestDmg:
                 break;*/
            case LockingTargetStrategy.LowestHp:
                if (currentTarget == null)
                    currentTarget = GetLowestHpTarget();
                break;

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

    DefenseBehaviour GetLowestHpTarget()
    {
        DefenseBehaviour lowestHpTarget = null;
        float lowestHp = Mathf.Infinity;
        foreach (DefenseBehaviour defenseBehaviour in defenseBehaviours)
        {
            float hp = defenseBehaviour.DefenseData.Hp;
            if (hp < lowestHp)
            {
                lowestHp = hp;
                lowestHpTarget = defenseBehaviour;
            }
        }
        return lowestHpTarget;
    }


}
