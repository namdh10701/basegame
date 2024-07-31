using System.Collections.Generic;
using CsvHelper.Configuration.Attributes;

namespace _Game.Scripts.GD.DataManager
{
    /// <summary>
    /// 
    /// </summary>
    public class MonsterTable : LocalDataTable<MonsterTableRecord>
    {
        public MonsterTable() : base("monsters_db.json")
        {
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public class MonsterTableRecord: DataTableRecord
    {
        [Index(0)]
        public string Id { get; set; }
        
        [Index(1)]
        public string Name { get; set; }
        
        [Index(2)]
        [Default(0)]
        public float PowerNumber { get; set; }
        
        [Index(3)]
        [Default(0)]
        public float FeverPoint { get; set; }
        
        [Index(4)]
        [Default(0)]
        public float Attack { get; set; }
        
        [Index(5)]
        [Default(0)]
        public float AttackSpeed { get; set; }
        
        [Index(6)]
        [Default(0)]
        public float Hp { get; set; }
        
        [Index(7)]
        [Default(0)]
        public float BlockChance { get; set; }
        
        [Index(8)]
        [Default(0)]
        public float AttackRange { get; set; }
        
        public override object GetId()
        {
            return Id;
        }
    }
}