using UnityEngine;

namespace _Base.Scripts.Shared
{
    public static class GlobalData
    {
        public const string GameUIScene = "GameUIScene";
        public const string GameScene = "GameScene";
        public const string FinalDemo = "FinalDemo";

        public static LayerMask PlayerLayer = LayerMask.NameToLayer("Player");
        public static LayerMask EnemyLayer = LayerMask.NameToLayer("Enemy");
    }
}