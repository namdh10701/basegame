
using _Base.Scripts.Database;
using System;
namespace Demo.Scripts

{
    [Serializable]
    public class DataDef : Record
    {
        public string Name;
    }

    [Serializable]
    public class HeroDef : Record
    {
        public string Atk;
        public AtkDef AtkDef;
    }


    [Serializable]
    public class AtkDef
    {
        public string Atk;
        public string Type;
    }
}