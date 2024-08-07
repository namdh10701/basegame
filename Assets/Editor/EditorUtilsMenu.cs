using UnityEngine;
using UnityEditor;
using System.IO;

namespace StormStudio.Common.Editor
{
    public static class UtilMenu
    {
        [MenuItem("StormStudio/Utils/Data/Clear Player Pref", false, 20)]
        public static void ClearPlayerPref()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("Cleared player preferences!");
        }

        [MenuItem("StormStudio/Utils/Data/Clear Persistant Data", false, 20)]
        public static void ClearPersistantData()
        {
            Debug.Log("Persistant data located at :" + Application.persistentDataPath);
            DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
            dir.Delete(true);
            Debug.Log("Deleted player persistant data!");
        }

        [MenuItem("StormStudio/Utils/Data/Clear temporary Data", false, 20)]
        public static void ClearTempData()
        {
            Debug.Log("Temporary data located at :" + Application.temporaryCachePath);
            DirectoryInfo dir = new DirectoryInfo(Application.temporaryCachePath);
            dir.Delete(true);
            Debug.Log("Deleted player temporary data!");
        }

        [MenuItem("StormStudio/Utils/Data/Clear All", false, 12)]
        public static void ClearAll()
        {
            ClearPlayerPref();
            ClearPersistantData();
            ClearTempData();
        }

        [MenuItem("StormStudio/Utils/Memory/UnloadAllUnusedAssets", false, 20)]
        public static void UnloadAllUnusedAsset()
        {
            Debug.Log("Unload all unused assets ");

            Resources.UnloadUnusedAssets();
            EditorUtility.UnloadUnusedAssetsImmediate();
        }
    }
}