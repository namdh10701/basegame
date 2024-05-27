using System;
using UnityEngine;

[Serializable]
public struct DeviableFloat
{
    public float BaseValue;
    public float Deviation;
    float value;
    public float Value => value;
    public DeviableFloat(float baseValue, float deviation)
    {
        BaseValue = baseValue;
        Deviation = deviation;
        value = BaseValue;
    }
    public void RefreshValue()
    {
        value = BaseValue + UnityEngine.Random.Range(-Deviation, Deviation);
    }
}

[Serializable]
public struct DeviableVector2
{
    public Vector2 BaseValue;
    public Vector2 Deviation;

    Vector2 value;
    public Vector2 Value => value;
    public void RefreshValue()
    {
        value = BaseValue + new Vector2(UnityEngine.Random.Range(-Deviation.x, Deviation.x), UnityEngine.Random.Range(-Deviation.y, Deviation.y));
    }
}
