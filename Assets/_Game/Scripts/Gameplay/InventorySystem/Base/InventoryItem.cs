using _Base.Scripts.Database;
using _Base.Scripts.SaveSystem;
using System.IO;
using UnityEngine;

namespace _Game.Scripts.InventorySystem
{
    public enum InventoryType
    {
        Potion, Gear, Crew
    }
    public class InventoryItem
    {
        public int Id;
        public InventoryType Type;
        public string Name;
        public InventoryItem()
        {

        }
        public InventoryItem(int id, string name)
        {
            Id = id;
            Type = InventoryType.Gear;
            Name = name;
        }
    }
}
