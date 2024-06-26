using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Game.Scripts;
using MBT;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Enemy/Approach")]
public class Approach : Leaf
{
    public EnemyReference Enemy;
    public ShipReference Ship;
    public float Force;

    float elapsedTime;
    public float ChangeDirectionInterval = 1;
    Vector2 direction;

    OctaDirectionRay OctarDirectionRay;
    public override void OnEnter()
    {
        base.OnEnter();
        UpdateTargetDirection();
    }

    public override NodeResult Execute()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > ChangeDirectionInterval)
        {
            elapsedTime = 0;
            UpdateTargetDirection();
        }

        Enemy.Value.body.AddForce(direction.normalized * Force);
        float distance = Vector2.Distance(Enemy.Value.transform.position, Ship.Value.Transform.position);
        return distance < 1 ? NodeResult.success : NodeResult.running;
    }


    void UpdateTargetDirection()
    {
        Vector3 targetPos = Ship.Value.ShipArea.SamplePoint();
        direction = targetPos - Enemy.Value.transform.position;
    }
}
