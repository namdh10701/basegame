using CsvHelper.Configuration.Attributes;

namespace _Game.Scripts.GD.DataManager
{
    /// <summary>
    /// 
    /// </summary>
    public class GameData
    {
        public static ShopItemTable ShopItemTable 
            = new("https://docs.google.com/spreadsheets/d/1lg1LNncm8kVlASyTctlJqWU7dw5JWz2SKmbD82R8uM0/edit?gid=0#gid=0");
        public static ShopListingTable ShopListingTable 
            = new("https://docs.google.com/spreadsheets/d/1lg1LNncm8kVlASyTctlJqWU7dw5JWz2SKmbD82R8uM0/edit?gid=1364617036#gid=1364617036");
        public static ShopRarityTable ShopRarityTable 
            = new("https://docs.google.com/spreadsheets/d/1lg1LNncm8kVlASyTctlJqWU7dw5JWz2SKmbD82R8uM0/edit?gid=809979341#gid=809979341");
        
        public static CrewTable CrewTable 
            = new("https://docs.google.com/spreadsheets/d/1M91hXkFM9BvP5SsfMKz-oDndDaOx-hJLfFTE39kfEJM/edit?gid=99718053#gid=99718053");
        public static CannonTable CannonTable 
            = new("https://docs.google.com/spreadsheets/d/1M91hXkFM9BvP5SsfMKz-oDndDaOx-hJLfFTE39kfEJM/edit?gid=1936044627#gid=1936044627");
        public static CannonTable CannonFeverTable 
            = new("https://docs.google.com/spreadsheets/d/1M91hXkFM9BvP5SsfMKz-oDndDaOx-hJLfFTE39kfEJM/edit?gid=1608471390#gid=1608471390", "CannonFeverTable");
        public static AmmoTable AmmoTable 
            = new("https://docs.google.com/spreadsheets/d/1M91hXkFM9BvP5SsfMKz-oDndDaOx-hJLfFTE39kfEJM/edit?gid=1915146529#gid=1915146529");
        public static ShipTable ShipTable 
            = new("https://docs.google.com/spreadsheets/d/1M91hXkFM9BvP5SsfMKz-oDndDaOx-hJLfFTE39kfEJM/edit?gid=1273073460#gid=1273073460");
        
        public static LevelWaveTable LevelWaveTable 
            = new ("https://docs.google.com/spreadsheets/d/16zvsN6iALnKVByPfI9BGuvyW44DHVhBZVg07CDoUyOY/edit?gid=755631495#gid=755631495");
        
        public static TalentTreeTable TalentTreeNormalTable 
            = new ("https://docs.google.com/spreadsheets/d/1M91hXkFM9BvP5SsfMKz-oDndDaOx-hJLfFTE39kfEJM/edit?gid=1839497694#gid=1839497694", "TalentTreeNormalTable");
        public static TalentTreeTable TalentTreePremiumTable 
            = new ("https://docs.google.com/spreadsheets/d/1M91hXkFM9BvP5SsfMKz-oDndDaOx-hJLfFTE39kfEJM/edit?gid=527127255#gid=527127255", "TalentTreePremiumTable");
        public static TalentTreeItemTable TalentTreeItemTable
            = new("https://docs.google.com/spreadsheets/d/1M91hXkFM9BvP5SsfMKz-oDndDaOx-hJLfFTE39kfEJM/edit?gid=1712483375#gid=1712483375");

        public static MonsterTable MonsterTable
            = new("https://docs.google.com/spreadsheets/d/1M91hXkFM9BvP5SsfMKz-oDndDaOx-hJLfFTE39kfEJM/edit?gid=2019962973#gid=2019962973");
    }
}