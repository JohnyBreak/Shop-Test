using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [HideInInspector] public SaveData SaveData;

    private void Awake()
    {
        Load();
    }

    public void Load() 
    {
        SaveData = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/saves/Save.save");

        if (SaveData == null)
        {
            SaveData = new SaveData();
            Debug.LogError("new saveData");
        }
        if(SaveData.ShopItemTypeTimePair == null) SaveData.ShopItemTypeTimePair = new List<ShopItemTypeTimePair>();
    }

    public void Save() 
    {
        SerializationManager.Save(SaveData);
    }
}
