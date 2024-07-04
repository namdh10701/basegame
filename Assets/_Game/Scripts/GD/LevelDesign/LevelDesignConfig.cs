using System.Collections.Generic;

namespace _Game.Scripts.GD
{
    [System.Serializable]
    public struct LevelDesignConfig
    {
        public string stage;
        public float time_offset;
        public List<string> enemy_ids;
        public int total_power;

        public override string ToString()
        {
            //string ret = stage + " " + enemy_ids.Count + " " + time_offset + " " + total_power;
            return "";
        }
    }
}
