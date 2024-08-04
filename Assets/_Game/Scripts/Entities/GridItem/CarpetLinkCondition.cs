using _Game.Features.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarpetLinkCondition
{
    List<CarpetType> carpetTypes = new List<CarpetType>();
    public CarpetLinkCondition(List<CarpetType> carpetTypes)
    {
        this.carpetTypes = carpetTypes;
    }
    public bool IsValid(Carpet carpet)
    {
        return carpetTypes.Contains(carpet.CarpetType);
    }
}
