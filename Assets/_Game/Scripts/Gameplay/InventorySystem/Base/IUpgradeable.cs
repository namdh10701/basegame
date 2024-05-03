using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.InventorySystem
{
    public enum Rarity
    {
        Common, Uncommon, Rare, Epic, Legend, LegendPlus
    }
    public interface IUpgradeable
    {
        public Rarity Rarity { get; set; }
    }
}
