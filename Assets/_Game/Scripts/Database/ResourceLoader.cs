using UnityEngine;

public static class ResourceLoader
{
    static string hatGearPath = "Database/Gears/Hat/Images";
    static string swordGearPath = "Database/Gears/Sword/Images";
    static string necklaceGearPath = "Database/Gears/Necklace/Images";
    static string characterPath = "Database/Characters/Captain/";

    public static Sprite LoadGearImage(GearKey gearKey)
    {
        string path = "";
        switch (gearKey.GearType)
        {
            case GearType.Necklace:
                path = necklaceGearPath;
                break;
            case GearType.Sword:
                path = swordGearPath;
                break;
            case GearType.Hat:
                path = hatGearPath;
                break;
        }
        path += $"/{gearKey.id}";
        return Resources.Load<Sprite>(path);
    }

    public static Sprite LoadCharacterImage(int id)
    {
        return Resources.Load<Sprite>(characterPath + $"/{id}");
    }
}