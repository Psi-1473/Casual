using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadManager
{
    public void SavePlayerData(Player player)
    {
        string path = Application.persistentDataPath + "/save.json";
        SaveData saveData = MakeSaveData(player);

        string data = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(path, data);
    }

    public bool LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/save.json";
        if (!File.Exists(path))
            return false;

        string data = File.ReadAllText(path);
        SaveData saveData = JsonUtility.FromJson<SaveData>(data);
        Managers.GetPlayer.PlayerName = saveData.playerName;
        Managers.GetPlayer.Inven.Gold = saveData.gold;
        Managers.GetPlayer.Inven.ExpStone = saveData.expStone;
        Managers.GetPlayer.StageComp.OpenedChapter = saveData.chapter;
        Managers.GetPlayer.StageComp.OpenedStage = saveData.stage;

        foreach(var heroData in saveData.herosData)
            Managers.GetPlayer.HeroComp.TakeSavedHero(heroData);
 

        foreach (var misc in saveData.miscData)
            Managers.GetPlayer.Inven.GainSavedItem(misc);

        foreach (var equip in saveData.equipData)
            Managers.GetPlayer.Inven.GainSavedItem(equip);

        return true;
    }

    SaveData MakeSaveData(Player player)
    {
        SaveData saveData = new SaveData();
        saveData.SetInfo(player);
        return saveData;
    }
}
