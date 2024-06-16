using _Game.Scripts;
using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[MBTNode("ElectricEel/UnderwaterMove")]
public class UnderwaterMove : Leaf
{
    public RigidbodyReference Rigidbody;
    public ElectricEel ElectricEel;
    public AreaReference moveArea;
    public int moveToPerform;
    public int moveCount;
    public float moveTimer;
    public float moveSpeed;

    public float currentMoveSpeed;
    public float targetMoveSpeed;


    public Vector2 nextPos;
    bool isMoving;

    float restTimer;
    public override void OnEnter()
    {
        base.OnEnter();
        moveCount = 0;
    }


    public override NodeResult Execute()
    {
        currentMoveSpeed = Mathf.Lerp(currentMoveSpeed, targetMoveSpeed, Time.fixedDeltaTime * 5);

        if (moveArea == null)
        {
            return NodeResult.success;
        }
        if (!isMoving)
        {
            isMoving = true;
            nextPos = moveArea.Value.SamplePoint(ElectricEel.transform.position, 3);
        }

        Vector2 direction = nextPos - (Vector2)ElectricEel.transform.position;
        Rigidbody.Value.velocity = direction.normalized * currentMoveSpeed;

        if (Vector2.Distance(ElectricEel.transform.position, nextPos) > 1.5f)
        {
            targetMoveSpeed = moveSpeed;
        }
        else
        {
            targetMoveSpeed = 0;
            restTimer += Time.fixedDeltaTime;
            if (restTimer > .5f)
            {
                restTimer = 0;
                isMoving = false;
                moveCount++;
            }
        }
        return moveCount < moveToPerform ? NodeResult.running : NodeResult.success;
    }

    public override void OnExit()
    {
    }
}
