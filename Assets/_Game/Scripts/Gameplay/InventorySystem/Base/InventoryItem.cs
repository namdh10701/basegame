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
    public class InventoryItem : IBinarySaveData
    {
        public InventoryId Id;
        public string Name;
        public InventoryItem()
        {
            Id = new InventoryId();
            Name = "";
        }
        public InventoryItem(InventoryId id, string name)
        {
            Id = id;
            Name = name;
        }

        public virtual void Read(BinaryReader br)
        {
            Id.Read(br);
            Name = br.ReadString();
        }
        public virtual void Write(BinaryWriter bw)
        {
            Id.Write(bw);
            bw.Write(Name);
        }
    }
    public class InventoryId : IBinarySaveData
    {
        public int Id;
        public InventoryType InventoryType;

        public InventoryId()
        {

        }
        public InventoryId(int id, InventoryType inventoryType)
        {
            Id = id;
            InventoryType = inventoryType;
        }

        public void Read(BinaryReader br)
        {
            Id = br.ReadInt32();
            InventoryType = (InventoryType)br.ReadInt32();
        }
        public void Write(BinaryWriter bw)
        {
            bw.Write(Id);
            bw.Write((int)InventoryType);
        }
    }


}
