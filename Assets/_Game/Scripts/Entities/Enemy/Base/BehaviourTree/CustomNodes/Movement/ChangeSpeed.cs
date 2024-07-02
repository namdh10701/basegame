using _Game.Scripts;
using MBT;
using UnityEngine;
[MBTNode("My Node/Change Speed")]
public class ChangeSpeed : Leaf
{
    [SerializeField] RigidbodyReference body;
    [SerializeField] Vector2Reference direction;
    [SerializeField] FloatReference drag;
    [SerializeField] float time;
    [SerializeField] float targetSpeed;
    float elapsedTime;
    float force;
    public override void OnEnter()
    {
        base.OnEnter();
        elapsedTime = 0;
        force = targetSpeed / Time.fixedDeltaTime * body.Value.mass / (time / Time.fixedDeltaTime);
        body.Value.drag = 0;
    }
    public override NodeResult Execute()
    {
        elapsedTime += Time.fixedDeltaTime;
        if (elapsedTime < time)
        {
            body.Value.AddForce(force * direction.Value, ForceMode2D.Force);
            return NodeResult.running;
        }
        else
        {
            return NodeResult.success;
        }

    }
    public override void OnExit()
    {
        base.OnExit();
        body.Value.drag = drag.Value;
    }

    public override void OnAllowInterrupt()
    {
        base.OnAllowInterrupt();

    }
}
