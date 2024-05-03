using _Game.Scripts;
using Newtonsoft.Json;
using System;

namespace _Base.Scripts.Database
{
    [Serializable]
    public class Record
    {
        public virtual Identifier Id { get; set; }
    }

    [Serializable]
    public class Identifier
    {
    }
}