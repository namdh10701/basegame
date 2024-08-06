using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Game.Scripts;
using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Enemy/Is Has Any Crew")]
public class IsHasAnyCrew : Condition
{
    public ShipReference shipReference;
    public override bool Check()
    {
        return shipReference.Value.ShipSetup.CrewController.crews.Count > 0;
    }
}
