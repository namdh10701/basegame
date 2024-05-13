using _Base.Scripts.RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace _Game.Scripts.InventorySystem
{
    public class Material : InventoryItem, IUpgradeable, IStackable
    {
        public Rarity Rarity { get; set; }
        public int Count { get; set; }

        public Material(int id, string name) : base(id, name)
        {

        }
    }
}
