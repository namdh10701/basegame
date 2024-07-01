using System;
using System.Collections.Generic;
using UnityEngine;
namespace _Base.Scripts.UI
{
    [CreateAssetMenu(menuName = "Scriptable Objects/AssetInfo")]
    public class AssetConfig : ScriptableObject
    {
        public List<AssetInfor> assetsConfig = new List<AssetInfor>();
    }

    [Serializable]
    public class AssetInfor
    {
        public Sprite sprite;
        public List<string> Ids;
    }

}
