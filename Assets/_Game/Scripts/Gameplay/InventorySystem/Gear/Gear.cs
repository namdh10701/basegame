using _Base.Scripts.RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace _Game.Scripts.InventorySystem
{
    public class Gear : InventoryItem, IUpgradeable
    {
        public GearType GearType;
        public Rarity Rarity { get; set; }
        public List<Stat> stats;

        public override void Read(BinaryReader br)
        {
            base.Read(br);
            GearType = (GearType)br.ReadInt32();
            Rarity = (Rarity)br.ReadInt32();
        }

        public override void Write(BinaryWriter bw)
        {
            base.Write(bw);
            bw.Write((int)GearType);
            bw.Write((int)Rarity);
        }
    }
}
