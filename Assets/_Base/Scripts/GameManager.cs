using UnityEngine;

namespace _Base.Scripts
{
    public abstract class BaseGameManager: MonoBehaviour
    {
        public abstract void LoadDatabase();
        public abstract void LoadSave();
        public abstract void SaveGame();
    }
}
