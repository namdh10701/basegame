
using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("My Node Decorator/Wait Interupable")]
public class WaitInterupable : Decorator
{
    public BoolReference BoolReference;
    public bool BoolValue;
    public FloatReference time = new FloatReference(1f);
    public float randomDeviation = 0f;
    public bool continueOnRestart = false;

    private float timer;

    public override void OnEnter()
    {
        if (!continueOnRestart)
        {
            timer = (randomDeviation == 0f) ? 0f : Random.Range(-randomDeviation, randomDeviation);
        }
    }

    public override NodeResult Execute()
    {
        if (timer >= time.Value || BoolReference.Value == BoolValue)
        {
            // Reset timer in case continueOnRestart option is active
            if (continueOnRestart)
            {
                timer = (randomDeviation == 0f) ? 0f : Random.Range(-randomDeviation, randomDeviation);
            }
            return NodeResult.success;
        }
        timer += this.DeltaTime;
        return NodeResult.running;
    }

    void OnValidate()
    {
        if (time.isConstant)
        {
            randomDeviation = Mathf.Clamp(randomDeviation, 0f, time.GetConstant());
        }
        else
        {
            randomDeviation = Mathf.Clamp(randomDeviation, 0f, 600f);
        }
    }

}
