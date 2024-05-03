using _Base.Scripts.SaveSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Game.Scripts.InventorySystem
{
    public class InventoryCollection<T> : IBinarySaveData where T : InventoryItem, new()
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
                T item = new T();
                item.Read(br);
                Items.Add(item);
            }
        }

        public void Write(BinaryWriter bw)
        {
            bw.Write(Items.Count);
            foreach (var item in Items)
            {
                item.Write(bw);
            }
        }
    }
}
