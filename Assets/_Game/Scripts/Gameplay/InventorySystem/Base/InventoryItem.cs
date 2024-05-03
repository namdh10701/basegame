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
