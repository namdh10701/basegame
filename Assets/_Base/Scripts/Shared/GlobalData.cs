using UnityEngine;

namespace _Base.Scripts.Shared
{
    public static class GlobalData
    {
        public static LayerMask PlayerLayer = LayerMask.NameToLayer("Player");
        public static LayerMask EnemyLayer = LayerMask.NameToLayer("Enemy");
        public static string MAP_CHANGED = "MAP_CHANGED";
    }
}