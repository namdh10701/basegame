using _Base.Scripts.Database;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace _Base.Scripts.Database
{
    public class Database<T> where T : Record
    {
        private string path;

        public Dictionary<int, T> Records;
        public Database(string path)
        {
            this.path = path;
        }
        public void Load()
        {
            TextAsset textAsset = Resources.Load<TextAsset>(path);
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(textAsset.text);
            Records = new Dictionary<int, T>();
            foreach (T item in wrapper.items)
            {
                Records.Add(item.Id, item);
            }
        }

        public T GetById(int Id)
        {
            return Records[Id];
        }
    }
}

[Serializable]
public class Wrapper<T> where T : Record
{
    public List<T> items;
}