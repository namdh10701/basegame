using _Base.Scripts.Database;
using _Base.Scripts.SaveSystem;
using System;
using System.IO;

[Serializable]
public class IntKey : Identifier
{
    public int Value;
    public IntKey(int value)
    {
        Value = value;
    }
    public override bool Equals(object obj)
    {
        return Value == ((IntKey)obj).Value;
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
public enum GearType
{
    Sword, Necklace, Hat
}

[Serializable]
public class GearKey : Identifier, IBinarySaveData
{
    public int id;
    public GearType GearType;
    public GearKey(int value, GearType gearType)
    {
        id = value;
        GearType = gearType;
    }
    public override bool Equals(object obj)
    {
        return id == ((GearKey)obj).id && GearType == ((GearKey)obj).GearType;
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public void Read(BinaryReader br)
    {
        
    }

    public void Write(BinaryWriter bw)
    {
    }
}