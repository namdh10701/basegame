using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.InventorySystem
{
    public enum Rarity
    {
        Common = 0,
        Uncommon = 1, Uncommon1 = 2,
        Rare = 3, Rare1 = 4, Rare2 = 5,
        Epic = 6, Epic1 = 7, Epic2 = 8,
        Legend = 9, Legend1 = 10
    }
    public interface IUpgradeable
    {
        public Rarity Rarity { get; set; }
    }
}
