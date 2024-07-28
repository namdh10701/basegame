using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using _Base.Scripts.Bootstrap;
using _Base.Scripts.Generators;
using Newtonsoft.Json;
using UnityEngine;

namespace _Game.Scripts.Bootstrap
{
    public class Game : BaseGame
    {
         public static IIdGenerator IDGenerator = new ShortUuidGenerator();
        public class MasterData
        {
            // https://docs.google.com/spreadsheets/d/1M91hXkFM9BvP5SsfMKz-oDndDaOx-hJLfFTE39kfEJM/export?gid=1915146529&format=csv
        }
        
        public class GSheetData
        {
            public string range;
            public string majorDimension;
            public List<List<string>> values;
        }

        public class CannonConf
        {
            public string ID;
            public string name;
            public string default_rarity;
            public float HP;
            public float attack;
            public float attack_speed;
            public float accuracy;
            public float crit_chance;
            public float crit_damage;
            public float range;
            public float skill;
            public float primary_project_dmg;
            public float secondary_project_dmg;
            public float project_count;
            public float angle;


            public class AmmoConf
            {
                public string ID;
                public string name;
                public string default_rarity;
                public float energy_cost;
                public float magazine_size;
                public float ammo_attack;
                public float attack_aoe;
                public float armor_pen;
                public float project_piercing;
                public float project_speed;
                public float ammo_accuracy;
                public float ammo_crit_chance;
                public float ammo_crit_damage;
                public float trigger_prob;
                public float duration;
                public float speed_modifer;
                public float dps;
                public float pierc_count;
                public float hp_threshold;
            }

#if UNITY_EDITOR
            [ContextMenu("Test", false)]
            protected void Test()
            {
                var data = @"
{
range: ""Cannon!A1:X1000"",
majorDimension: ""ROWS"",
values: [
[
""ID"",
""name"",
""HP"",
""attack"",
""attack_speed"",
""accuracy"",
""crit_chance"",
""crit_damage"",
""range"",
""skill"",
""primary_project_dmg"",
""secondary_project_dmg"",
""project_count"",
""angle""
],
[
""001"",
""fast"",
""250"",
""100"",
""1"",
""5"",
""0.2"",
""1.5""
],
[
""002"",
""normal"",
""300"",
""50"",
""1.5"",
""10"",
""0.1"",
""1.25""
],
[
""003"",
""far dmg""
],
[
""004"",
""close dmg""
],
[
""005"",
""charge shot""
],
[
""006"",
""twin shot"",
"""",
"""",
"""",
"""",
"""",
"""",
"""",
"""",
""0.5""
],
[
""007"",
""split shot""
],
[
""008"",
""fork""
],
[
""009"",
""velkoz""
],
[
""010"",
""chaining""
]
]
}
";

                var dict = new Dictionary<string, CannonConf>();
                var gSheetData = JsonConvert.DeserializeObject<GSheetData>(data);
                Debug.Log(gSheetData);

                foreach (var list in gSheetData.values.Skip(1))
                {
                    var colIdx = 0;
                    dict[list[0]] = new CannonConf();
                    foreach (var fieldInfo in dict[list[0]].GetType().GetFields())
                    {
                        object value = "0";

                        if (colIdx < list.Count)
                        {
                            value = list[colIdx++];

                            if (string.IsNullOrEmpty(value.ToString()))
                            {
                                value = "0";
                            }
                        }

                        if (fieldInfo.FieldType == typeof(float))
                        {
                            value = float.Parse(value.ToString(), CultureInfo.InvariantCulture);
                        }

                        fieldInfo.SetValue(dict[list[0]], value);
                    }
                }

                Debug.Log("abc");
            }
#endif
        }
    }
}