using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using _Base.Scripts.Utils.Extensions;
using Cysharp.Threading.Tasks;
using Online;

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

        public static CrewTable CrewTable => PlayfabManager.Instance.CrewTable;
        public static CannonTable CannonTable => PlayfabManager.Instance.CannonTable;
        public static CannonTable CannonFeverTable => PlayfabManager.Instance.CannonFeverTable;
        public static AmmoTable AmmoTable => PlayfabManager.Instance.AmmoTable;
        public static ShipTable ShipTable => PlayfabManager.Instance.ShipTable;
        
        public static LevelWaveTable LevelWaveTable 
            = new ("https://docs.google.com/spreadsheets/d/16zvsN6iALnKVByPfI9BGuvyW44DHVhBZVg07CDoUyOY/edit?gid=755631495#gid=755631495");
        
        public static TalentTreeTable TalentTreeNormalTable 
            = new ("https://docs.google.com/spreadsheets/d/1M91hXkFM9BvP5SsfMKz-oDndDaOx-hJLfFTE39kfEJM/edit?gid=1839497694#gid=1839497694", "TalentTreeNormalTable");
        public static TalentTreeTable TalentTreePremiumTable 
            = new ("https://docs.google.com/spreadsheets/d/1M91hXkFM9BvP5SsfMKz-oDndDaOx-hJLfFTE39kfEJM/edit?gid=527127255#gid=527127255", "TalentTreePremiumTable");
        public static TalentTreeItemTable TalentTreeItemTable
            = new("https://docs.google.com/spreadsheets/d/1M91hXkFM9BvP5SsfMKz-oDndDaOx-hJLfFTE39kfEJM/edit?gid=1712483375#gid=1712483375");

        public static MonsterTable MonsterTable => PlayfabManager.Instance.MonsterTable;
        
        public static InventoryItemUpgradeTable CannonUpgradeTable
            = new("https://docs.google.com/spreadsheets/d/1GT5jPQFREA2wldlQkaVaSaFwYkfeBS__LDfkTjgzimM/edit?gid=1522252696#gid=1522252696");
        
        public static InventoryItemUpgradeTable AmmoUpgradeTable
            = new("https://docs.google.com/spreadsheets/d/1GT5jPQFREA2wldlQkaVaSaFwYkfeBS__LDfkTjgzimM/edit?gid=150248782#gid=150248782");
        
        public static InventoryItemUpgradeTable ShipUpgradeTable
            = new("https://docs.google.com/spreadsheets/d/1GT5jPQFREA2wldlQkaVaSaFwYkfeBS__LDfkTjgzimM/edit?gid=1305386051#gid=1305386051");
        
        // public static DataTable<TTable> Get<TTable>()
        // {
        //     
        // }
        
        
        public static UniTask Load()
        {
            return TaskUtils.WaitAllWithConcurrencyControl(new List<Func<UniTask>>
            {
                () => ShopItemTable.LoadData(),
                () => ShopListingTable.LoadData(),
                () => ShopRarityTable.LoadData(),
                () => CrewTable.LoadData(),
                () => CannonTable.LoadData(),
                () => CannonFeverTable.LoadData(),
                () => AmmoTable.LoadData(),
                () => ShipTable.LoadData(),
                () => LevelWaveTable.LoadData(),
                () => TalentTreeNormalTable.LoadData(),
                () => TalentTreePremiumTable.LoadData(),
                () => TalentTreeItemTable.LoadData(),
                () => MonsterTable.LoadData(),

                () => CannonUpgradeTable.LoadData(),
                () => AmmoUpgradeTable.LoadData(),
                () => ShipUpgradeTable.LoadData(),

                () => GDConfigLoader.Instance.Load(),
            }, 5);
            
            
            
            var xxx = typeof(GameData).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(v => IsSubclassOfGeneric(v.FieldType, typeof(DataTable<>)))
                .ToList();
            var tasks = typeof(GameData).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(v => IsSubclassOfGeneric(v.FieldType, typeof(DataTable<>)))
                .Select(v =>
                {
                    var table = v.GetValue(null) as DataTable<DataTableRecord>;
                    UniTask Factory() => table!.LoadData();
                    return (Func<UniTask>)Factory;
                });
            return TaskUtils.WaitAllWithConcurrencyControl(tasks.Append(() => GDConfigLoader.Instance.Load()));
        }
        
        public static bool IsSubclassOfGeneric(Type type, Type genericTypeDefinition)
        {
            if (!genericTypeDefinition.IsGenericType || genericTypeDefinition.IsGenericTypeDefinition)
            {
                throw new ArgumentException("genericTypeDefinition must be a generic type definition", nameof(genericTypeDefinition));
            }

            // Check if the type itself matches the generic type definition
            if (type.IsGenericType && type.GetGenericTypeDefinition() == genericTypeDefinition)
            {
                return true;
            }

            // Check the type hierarchy for any matches
            while (type != null && type != typeof(object))
            {
                var currentType = type.IsGenericType ? type.GetGenericTypeDefinition() : null;
                if (currentType == genericTypeDefinition)
                {
                    return true;
                }

                // Check the base type
                type = type.BaseType;
            }

            return false;
        }
        
        // public static async Task Load()
        // {
        //     await ShopItemTable.LoadData();
        //     await ShopListingTable.LoadData();
        //     await ShopRarityTable.LoadData();
        //     await CrewTable.LoadData();
        //     await CannonTable.LoadData();
        //     await CannonFeverTable.LoadData();
        //     await AmmoTable.LoadData();
        //     await ShipTable.LoadData();
        //     await LevelWaveTable.LoadData();
        //     await TalentTreeNormalTable.LoadData();
        //     await TalentTreePremiumTable.LoadData();
        //     await TalentTreeItemTable.LoadData();
        //     await MonsterTable.LoadData();
        //     
        //     await CannonUpgradeTable.LoadData();
        //     await AmmoUpgradeTable.LoadData();
        //     await ShipUpgradeTable.LoadData();
        //
        //     await GDConfigLoader.Instance.Load();
        // }
    }
}