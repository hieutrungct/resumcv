using System;
using System.Collections;
using System.Collections.Generic;
using RubikCasual.DailyItem;
using UnityEngine;
using Rubik_Casual;
using CharacterInfo = Rubik_Casual.CharacterInfo;

public class SaveData : MonoBehaviour
{
    public CharacterInfo myData;
    
    void Start()
    {
        SaveDataToJson();
        // SaveDataToScriptableObject();
    }

    void SaveDataToJson()
    {
        // Chuyển đổi ScriptableObject thành JSON
        string json = JsonUtility.ToJson(myData);

        // Lưu JSON vào tệp

        System.IO.File.WriteAllText(Application.dataPath + "/_Rubik-Casual/ScriptableObject/data.json", json);
    }

    public void OnListCharater()
    {
        gameObject.SetActive(true);
        
        HUDController.instanse.UpdateTopPanel(Energe:false,Gold:false,Gem:false);
    }
    public void HideListCharater()
    {
        gameObject.SetActive(false);
        HUDController.instanse.UpdateTopPanel(Energe:true,Gold:true,Gem:true);
    }
}
  

