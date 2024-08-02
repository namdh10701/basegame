using System.Collections.Generic;
using UnityEngine;

namespace _Base.Scripts.Utils
{
    public class CachedResources
    {
        private static readonly Dictionary<string, Object> Cache = new();
        public static T Load<T>(string path) where T : Object
        {
            if (!Cache.ContainsKey(path))
                Cache[path] = Resources.Load<T>(path);
            return (T)Cache[path];
        }
    }
}