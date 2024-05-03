using _Base.Scripts.SaveSystem;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace _Game.Scripts.SaveLoad
{
    public class SaveList<T> : IBinarySaveData where T : IBinarySaveData, new()
    {
        public List<T> Items;
        public SaveList()
        {
            Items = new List<T>();
        }

        public void Read(BinaryReader br)
        {
            int count = br.ReadInt32();
            Debug.Log("READ SAVE LIST" + count);
            for (int i = 0; i < count; i++)
            {
                T item = new T();
                item.Read(br);
                Items.Add(item);
            }
        }

        public void Write(BinaryWriter bw)
        {
            bw.Write(Items.Count);
            Debug.Log("SAVE SAVE LIST" + Items.Count);
            foreach (T item in Items)
            {
                item.Write(bw);
            }
        }
    }
}
