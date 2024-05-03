using _Base.Scripts.SaveSystem;
using _Game.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GearCombination : IBinarySaveData
{
    public List<GearDefinition> GearDefinitions;

    public GearCombination()
    {
        GearDefinitions = new List<GearDefinition>();
    }

    public void Read(BinaryReader br)
    {
        int GearCount = br.ReadInt32();
        for (int i = 0; i < GearCount; i++)
        {
            GearDefinition gearDef = new GearDefinition();
            gearDef.Read(br);
            GearDefinitions.Add(gearDef);
        }
    }

    public void Write(BinaryWriter bw)
    {
        bw.Write(GearDefinitions.Count);
        foreach (var gearDef in GearDefinitions)
        {
            gearDef.Write(bw);
        }
    }
}
