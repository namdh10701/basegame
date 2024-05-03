using _Game.Scripts;
using Newtonsoft.Json;
using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace _Base.Scripts.Database
{
    [Serializable]
    public class Record
    {
        [JsonConverter(typeof(IdentifierConverter))]
        public Identifier Id;
    }

    [Serializable]
    public class Identifier
    {
    }
}