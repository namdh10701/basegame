using _Base.Scripts.RPG.Attributes;
using _Game.Scripts;
using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Enemy/Approach")]
public class Approach : Leaf
{
    public EnemyReference Enemy;
    public ShipReference Ship;
    public StatReference Force;
    public float multiplier = 1;
    float elapsedTime;
    public float ChangeDirectionInterval = 1;
    Vector2 direction;

    public bool isApproachClosest;
    OctaDirectionRay OctarDirectionRay;
    public float distanceMin = 1;
    public override void OnEnter()
    {
        base.OnEnter();
        UpdateTargetDirection();
    }

    public override NodeResult Execute()
    {
        elapsedTime += Time.deltaTime;
/*        if (elapsedTime > ChangeDirectionInterval)
        {
            elapsedTime = 0;
            UpdateTargetDirection();
        }*/
        direction = (Vector3)Ship.Value.ShipArea.ClosetPointTo(transform.position) - Enemy.Value.transform.position;

        Enemy.Value.Body.AddForce(direction.normalized * Force.Value.Value * multiplier);
        float distance = Vector2.Distance(Enemy.Value.transform.position, Ship.Value.transform.position);
        return distance < distanceMin ? NodeResult.success : NodeResult.running;
    }


    void UpdateTargetDirection()
    {
        Vector3 targetPos;
        if (!isApproachClosest)
            targetPos = Ship.Value.ShipArea.SamplePointUpSide();
        else
            targetPos = Ship.Value.ShipArea.ClosetPointTo(transform.position);
        direction = targetPos - Enemy.Value.transform.position;
    }
}
