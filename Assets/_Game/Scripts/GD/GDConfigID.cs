using _Game.Scripts.InventorySystem;
using System.Collections.Generic;

public enum CannonType
{
    Normal, Fast, FarDmg, CloseDmg, TwinShot, SplitShot, Fork, Velkoz, Chaining, ChargeShot
}

public static class GDConfigId
{
    public static Dictionary<KeyValuePair<CannonType, Rarity>, string> CannonIDs = new Dictionary<KeyValuePair<CannonType, Rarity>, string>()
{
    { new KeyValuePair<CannonType, Rarity>(CannonType.Fast, Rarity.Common), "0001" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.Fast, Rarity.Uncommon), "0002" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.Fast, Rarity.Uncommon1), "0003" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.Fast, Rarity.Rare), "0004" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.Fast, Rarity.Rare1), "0005" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.Fast, Rarity.Rare2), "0006" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.Fast, Rarity.Epic), "0007" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.Fast, Rarity.Epic1), "0008" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.Fast, Rarity.Epic2), "0009" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.Fast, Rarity.Legend), "0010" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.Fast, Rarity.Legend1), "0011" },

    { new KeyValuePair<CannonType, Rarity>(CannonType.Normal, Rarity.Common), "0012" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.Normal, Rarity.Uncommon), "0013" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.Normal, Rarity.Uncommon1), "0014" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.Normal, Rarity.Rare), "0015" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.Normal, Rarity.Rare1), "0016" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.Normal, Rarity.Rare2), "0017" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.Normal, Rarity.Epic), "0018" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.Normal, Rarity.Epic1), "0019" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.Normal, Rarity.Epic2), "0020" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.Normal, Rarity.Legend), "0021" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.Normal, Rarity.Legend1), "0022" },

    { new KeyValuePair<CannonType, Rarity>(CannonType.FarDmg, Rarity.Uncommon), "0023" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.FarDmg, Rarity.Uncommon1), "0024" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.FarDmg, Rarity.Rare), "0025" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.FarDmg, Rarity.Rare1), "0026" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.FarDmg, Rarity.Rare2), "0027" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.FarDmg, Rarity.Epic), "0028" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.FarDmg, Rarity.Epic1), "0029" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.FarDmg, Rarity.Epic2), "0030" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.FarDmg, Rarity.Legend), "0031" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.FarDmg, Rarity.Legend1), "0032" },

    { new KeyValuePair<CannonType, Rarity>(CannonType.CloseDmg, Rarity.Uncommon), "0033" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.CloseDmg, Rarity.Uncommon1), "0034" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.CloseDmg, Rarity.Rare), "0035" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.CloseDmg, Rarity.Rare1), "0036" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.CloseDmg, Rarity.Rare2), "0037" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.CloseDmg, Rarity.Epic), "0038" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.CloseDmg, Rarity.Epic1), "0039" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.CloseDmg, Rarity.Epic2), "0040" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.CloseDmg, Rarity.Legend), "0041" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.CloseDmg, Rarity.Legend1), "0042" },

    { new KeyValuePair<CannonType, Rarity>(CannonType.TwinShot, Rarity.Uncommon), "0043" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.TwinShot, Rarity.Uncommon1), "0044" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.TwinShot, Rarity.Rare), "0045" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.TwinShot, Rarity.Rare1), "0046" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.TwinShot, Rarity.Rare2), "0047" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.TwinShot, Rarity.Epic), "0048" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.TwinShot, Rarity.Epic1), "0049" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.TwinShot, Rarity.Epic2), "0050" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.TwinShot, Rarity.Legend), "0051" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.TwinShot, Rarity.Legend1), "0052" },

    { new KeyValuePair<CannonType, Rarity>(CannonType.TwinShot, Rarity.Epic2), "0050" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.TwinShot, Rarity.Legend), "0051" },
    { new KeyValuePair<CannonType, Rarity>(CannonType.TwinShot, Rarity.Legend1), "0052" },
    };
}
