using _Base.Scripts.RPG.Effects;
using _Game.Features.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarpetEffectHandler : MonoBehaviour
{
    public Carpet carpet;
    public void Apply(Effect effect, Cell cell)
    {
        foreach (CarpetComponent cc in carpet.components)
        {
            if (cc.OccupyCells.Contains(cell))
            {
                cc.EffectHandler.Apply(effect);
            }
        }
    }
}
