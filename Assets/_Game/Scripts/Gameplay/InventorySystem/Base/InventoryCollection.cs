using _Base.Scripts.SaveSystem;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace _Game.Scripts.InventorySystem
{
/*    public class InventoryCollection<T> : IBinarySaveData where T : InventoryItem, new()
    {
        public List<T> Items;

        public InventoryCollection()
        {
            Items = new List<T>();
        }

        public void Read(BinaryReader br)
        {
            int itemCount = br.ReadInt32();
            for (int i = 0; i < itemCount; i++)
            {
                T item = new();
               // item.Read(br);
               *//* if (item.Id.InventoryType == InventoryType.Gear)
                {
                    Gear gear = new Gear();
                    gear.Id = item.Id;
                    gear.Name = item.Name;
                    gear.Read(br);
                    item = gear;
                }*//*
                Items.Add(item);
            }
        }

        public void Write(BinaryWriter bw)
        {
            bw.Write(Items.Count);
            foreach (var item in Items)
            {
               // item.Write(bw);
            }
        }
    }*/
}
