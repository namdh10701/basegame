using _Base.Scripts.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace _Base.Scripts.Database
{
    public class Database<T> where T : Record
    {
        private string path;

        public Dictionary<Identifier, T> Records = new Dictionary<Identifier, T>();
        public Database(string path)
        {
            this.path = path;
        }
        public Database()
        {

        }
        public void Load()
        {
            TextAsset textAsset = Resources.Load<TextAsset>(path);
            Wrapper<T> wrapper = JsonConvert.DeserializeObject<Wrapper<T>>(textAsset.text);
            foreach (T item in wrapper.items)
            {
                Records.Add(item.Id, item);
            }
        }

        public T GetById(Identifier Id)
        {
            foreach (KeyValuePair<Identifier, T> item in Records)
            {
                if (item.Key.Equals(Id))
                {
                    return item.Value;
                }
            }
            return default;
        }
    }
}


[Serializable]
public class Wrapper<T> where T : Record
{
    public List<T> items;
}