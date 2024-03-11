using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : AbstractSingleton<GameManager>
{
    public Database Database;
    public SaveData SaveData;
    protected override void Awake()
    {
        base.Awake();
    }
    public void LoadDatabase()
    {
        Database.Load();
    }
    public void LoadSave()
    {
        SaveData = SaveLoadManager.ReadSave(1);
        if (SaveData == null)
        {
            SaveLoadManager.WriteDefaultSave(new SaveData(1, 0));
            SaveData = SaveLoadManager.ReadSave(1);
        }

        Debug.Log(SaveData.Coin);
    }
    public void SaveGame()
    {
        SaveLoadManager.WriteSave(SaveData);
    }
}
