using _Base.Scripts.RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace _Game.Scripts.InventorySystem
{
    public class Material : InventoryItem, IUpgradeable, IStackable
    {
        public Rarity Rarity { get; set; }
        public int Count { get; set; }

        public override void Read(BinaryReader br)
        {
            base.Read(br);
            Rarity = (Rarity)br.ReadInt32();
            Count = br.ReadInt32();
        }

        public override void Write(BinaryWriter bw)
        {
            base.Write(bw);
            bw.Write((int)Rarity);
            bw.Write(Count);
        }
    }
}
