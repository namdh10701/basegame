using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    public class Gear : ICraftItem
    {
        public GearDefinition GearDefinition;
        public Rarity Rarity { get => GearDefinition.Rarity; set => GearDefinition.Rarity = value; }
        public Gear(GearDefinition gearDefinition)
        {
            GearDefinition = gearDefinition;
        }
    }
}
