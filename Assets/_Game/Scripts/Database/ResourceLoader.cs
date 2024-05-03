using UnityEngine;

public static class ResourceLoader
{
    static string hatGearPath = "Database/Gears/Hat/Images";
    static string swordGearPath = "Database/Gears/Sword/Images";
    static string necklaceGearPath = "Database/Gears/Necklace/Images";
    static string characterPath = "Database/Characters/Captain";
    static string skillPath = "Database/Skills/Images";
    public static Sprite LoadGearImage(int id, GearType gearType)
    {
        string path = "";
        switch (gearType)
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
        path += $"/{id}";
        return Resources.Load<Sprite>(path);
    }

    public static Sprite LoadCharacterImage(int id)
    {
        return Resources.Load<Sprite>(characterPath + $"/{id}");
    }

    public static Sprite LoadSkillImage(int id)
    {
        return Resources.Load<Sprite>(skillPath + $"/{id}");
    }
}