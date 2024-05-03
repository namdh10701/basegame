using _Base.Scripts.RPG.Stats;
using _Base.Scripts.SaveSystem;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace _Game.Scripts
{
    public class GearDefinition : IBinarySaveData
    {
        public GearKey Id;
        public List<Stat> stats;
        public Rarity Rarity { get; set; }

        public void Read(BinaryReader br)
        {
            Id.Read(br);
        }

        public void Write(BinaryWriter bw)
        {
            Id.Write(bw);
        }
    }
}
