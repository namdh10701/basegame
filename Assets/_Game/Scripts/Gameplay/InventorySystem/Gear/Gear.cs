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
        public Gear() : base()
        {

        }
        public Gear(InventoryId id, string name, GearType gearType, Rarity rarity, List<Stat> stats) : base(id, name)
        {

            GearType = gearType;
            Rarity = rarity;
            this.stats = stats;
        }

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
