using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public interface IBinarySaveData
{

    void Write(BinaryWriter bw);
    void Read(BinaryReader br);
}

public static class BinarySave
{
    public static void Write<T>(this List<T> binaryDataList, BinaryWriter bw) where T : class, IBinarySaveData, new()
    {
      /*  var dataToSave = binaryDataList.FindAll(item => item.IsMeetingSaveCriteria);

        bw.Write(dataToSave.Count);
        dataToSave.ForEach(item => item.Write(bw));*/
    }


    public static void Write<T>(this List<List<T>> binaryDataLists, BinaryWriter bw) where T : class, IBinarySaveData, new()
    {
        bw.Write(binaryDataLists.Count);
        binaryDataLists.ForEach(item => item.Write(bw));
    }


    public static void Write(this Dictionary<string, int> binaryDataDictionary, BinaryWriter bw)
    {
        bw.Write(binaryDataDictionary.Count);

        foreach (var entry in binaryDataDictionary)
        {
            bw.Write(entry.Key);
            bw.Write(entry.Value);
        }
    }

    public static void Read(this Dictionary<string, int> binaryDataDictionary, BinaryReader br)
    {
        binaryDataDictionary.Clear();

        int itemCount = br.ReadInt32();
        for (int i = 0; i < itemCount; i++)
            binaryDataDictionary.Add(br.ReadString(), br.ReadInt32());
    }

    public static void Write(this List<int> binaryDataList, BinaryWriter bw)
    {
        bw.Write(binaryDataList.Count);
        binaryDataList.ForEach(bw.Write);
    }

    public static void Read(this List<int> binaryDataList, BinaryReader br)
    {
        binaryDataList.Clear();
        int itemCount = br.ReadInt32();
        for (int i = 0; i < itemCount; i++)
            binaryDataList.Add(br.ReadInt32());
    }

    public static void Write(this List<string> binaryDataList, BinaryWriter bw)
    {
        bw.Write(binaryDataList.Count);
        for (int i = 0; i < binaryDataList.Count; i++)
            bw.Write(binaryDataList[i] ?? "");
    }

    public static void Read(this List<string> binaryDataList, BinaryReader br)
    {
        binaryDataList.Clear();
        int itemCount = br.ReadInt32();
        for (int i = 0; i < itemCount; i++)
            binaryDataList.Add(br.ReadString());
    }

    public static void Write(this List<bool> binaryDataList, BinaryWriter bw)
    {
        bw.Write(binaryDataList.Count);
        for (int i = 0; i < binaryDataList.Count; i++)
            bw.Write(binaryDataList[i]);
    }

    public static void Read(this List<bool> binaryDataList, BinaryReader br)
    {
        binaryDataList.Clear();
        int itemCount = br.ReadInt32();
        for (int i = 0; i < itemCount; i++)
            binaryDataList.Add(br.ReadBoolean());
    }

}