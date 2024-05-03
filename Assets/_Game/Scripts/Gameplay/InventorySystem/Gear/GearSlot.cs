using _Base.Scripts.SaveSystem;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace _Game.Scripts.InventorySystem
{
    public class GearSlot : IBinarySaveData
    {
        public GearType GearType;
        public Gear Gear;
        public GearSlot()
        {
            Gear = new Gear();
        }
        public GearSlot(GearType gearType, Gear gear)
        {
            GearType = gearType;
            Gear = gear;
        }

        public void Read(BinaryReader br)
        {

            Debug.Log("READ GEAR SLOT");
            GearType = (GearType)br.ReadInt32();
            Gear.Read(br);
            Debug.Log(Gear.Name);
        }

        public void Write(BinaryWriter bw)
        {
            Debug.Log("WIRTE GEAR SLOT");
            Debug.Log(Gear.Name);
            bw.Write((int)GearType);
            Gear.Write(bw);
        }
    }
}