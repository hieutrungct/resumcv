using System;
using System.Collections;
using System.Collections.Generic;
using RubikCasual.DailyItem;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public ItemData myData;
    public TextAsset jsonFile;
    public ItemData saveDataToScriptableObject;
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
    void SaveDataToScriptableObject()
    {
        // Đọc dữ liệu từ tệp JSON
        string jsonText = jsonFile.text;

        RootData rootData = JsonUtility.FromJson<RootData>(jsonText);

        // Tạo một ScriptableObject mới và gán dữ liệu từ RootData

        saveDataToScriptableObject.InfoItems = ConvertToInfoItems(rootData.InfoItems);
        saveDataToScriptableObject.daySlots = ConvertToDaySlots(rootData.daySlots);
        saveDataToScriptableObject.daySlotWeeks = ConvertToDaySlotWeeks(rootData.daySlotWeeks);
        saveDataToScriptableObject.datalobby = ConvertToDataLobby(rootData.datalobby);

    }
    private RubikCasual.DailyItem.infoItem[] ConvertToInfoItems(Item[] items)
    {
        List<RubikCasual.DailyItem.infoItem> infoItems = new List<RubikCasual.DailyItem.infoItem>();
        foreach (var item in items)
        {
            RubikCasual.DailyItem.infoItem infoItem = new RubikCasual.DailyItem.infoItem();
            // Chuyển đổi các giá trị từ Item sang infoItem
            infoItem.id = item.id;
            infoItem.name = item.name;
            infoItem.Skin = item.Skin;
            infoItem.numberItem = item.numberItem;
            infoItem.imageItem = item.imageItem;
            // Chuyển đổi các giá trị khác tương tự
            infoItems.Add(infoItem);
        }

        return infoItems.ToArray();
    }

    private RubikCasual.DailyItem.DaySlot[] ConvertToDaySlots(DaySlot[] daySlots)
    {
        List<RubikCasual.DailyItem.DaySlot> DaySlots = new List<RubikCasual.DailyItem.DaySlot>();
        foreach (var item in daySlots)
        {
            RubikCasual.DailyItem.DaySlot DaySlot = new RubikCasual.DailyItem.DaySlot();
            // Chuyển đổi các giá trị từ Item sang infoItem
            DaySlot.idItem = item.idItem;
            DaySlot.isClick = item.isClick;
            DaySlot.isToday = item.isToday;
            DaySlot.numberItemBonus = item.numberItemBonus;
            DaySlot.tomorrow = item.tomorrow;

            // Chuyển đổi các giá trị khác tương tự
            DaySlots.Add(DaySlot);
        }

        return DaySlots.ToArray();
    }

    private RubikCasual.DailyItem.DaySlotWeek[] ConvertToDaySlotWeeks(DaySlotWeek[] daySlotWeeks)
    {
        List<RubikCasual.DailyItem.DaySlotWeek> DaySlotWeeks = new List<RubikCasual.DailyItem.DaySlotWeek>();
        foreach (var item in daySlotWeeks)
        {
            RubikCasual.DailyItem.DaySlotWeek DaySlotWeek = new RubikCasual.DailyItem.DaySlotWeek();
            // Chuyển đổi các giá trị từ Item sang infoItem
            DaySlotWeek.idItem = item.idItem;
            DaySlotWeek.isClick = item.isClick;
            DaySlotWeek.isToday = item.isToday;
            DaySlotWeek.numberItemBonus = item.numberItemBonus;
            DaySlotWeek.tomorrow = item.tomorrow;

            // Chuyển đổi các giá trị khác tương tự
            DaySlotWeeks.Add(DaySlotWeek);
        }

        return DaySlotWeeks.ToArray();
    }

    private RubikCasual.DailyItem.DataLobby[] ConvertToDataLobby(DataLobby[] dataLobby)
    {
        List<RubikCasual.DailyItem.DataLobby> DataLobby = new List<RubikCasual.DailyItem.DataLobby>();
        foreach (var item in dataLobby)
        {
            RubikCasual.DailyItem.DataLobby DataForLobby = new RubikCasual.DailyItem.DataLobby();
            // Chuyển đổi các giá trị từ Item sang infoItem
            DataForLobby.id = item.id;
            DataForLobby.name = item.name;
            DataForLobby.Skin = item.Skin;
            DataForLobby.numberItem = item.numberItem;
            DataForLobby.imageItem = item.imageItem;
            // Chuyển đổi các giá trị khác tương tự
            DataLobby.Add(DataForLobby);
        }

        return DataLobby.ToArray();
    }
}
[System.Serializable]
public class Item
{
    public int id;
    public string name;
    public string Skin;
    public int numberItem;
    public Sprite imageItem;
}

[System.Serializable]
public class ImageItem
{
    public int instanceID;
}

[System.Serializable]
public class DaySlot
{
    public int idItem;
    public int numberItemBonus;
    public bool isToday;
    public bool tomorrow;
    public bool isClick;
}

[System.Serializable]
public class DaySlotWeek
{
    public int idItem;
    public int numberItemBonus;
    public int DayPresent;
    public bool isToday;
    public bool tomorrow;
    public bool isClick;
}

[System.Serializable]
public class DataLobby
{
    public int id;
    public string name;
    public string Skin;
    public int numberItem;
    public Sprite imageItem;
}

[System.Serializable]
public class RootData
{
    public Item[] InfoItems;
    public DaySlot[] daySlots;
    public DaySlotWeek[] daySlotWeeks;
    public DataLobby[] datalobby;
}

