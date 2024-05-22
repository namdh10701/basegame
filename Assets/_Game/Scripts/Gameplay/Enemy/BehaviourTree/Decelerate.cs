using _Game.Scripts.Battle;
using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[MBTNode("My Node/Decelerate")]
[AddComponentMenu("")]
public class Decelerate : Leaf
{
    public Rigidbody2D body;
    public DeviableFloat TargetSpeed;
    public DeviableFloat Duration;

    float startSpeed;
    float elapsedTime;
    public override void OnEnter()
    {
        base.OnEnter();
        TargetSpeed.RefreshValue();
        Duration.RefreshValue();
        elapsedTime = 0;
        startSpeed = body.velocity.magnitude;
    }
    public override NodeResult Execute()
    {
        elapsedTime += Time.deltaTime;
        float speed = Mathf.Lerp(startSpeed, TargetSpeed.Value, elapsedTime / Duration.Value);

        Vector2 currentVel = body.velocity * speed;
        body.velocity = currentVel;
        return elapsedTime < Duration.Value ? NodeResult.running : NodeResult.success;
    }


}
