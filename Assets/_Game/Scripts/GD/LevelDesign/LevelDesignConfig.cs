using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
namespace _Game.Scripts.GD
{
    [System.Serializable]
    public struct LevelDesignConfig
    {
        public string stage;
        public float time_offset;
        public string enemy_id;

        public override string ToString()
        {
            string ret = stage + " " + enemy_id + " " + time_offset;
            return ret;

        }
    }
}
