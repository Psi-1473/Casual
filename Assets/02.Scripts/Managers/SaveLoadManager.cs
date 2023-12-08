using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadManager
{
    public void SaveHeroData()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        List<Hero> heroInfo = Managers.GetPlayer.HeroComp.Heros;
        List<HeroSaveData> herosData = new List<HeroSaveData>();

        for (int i = 0; i < heroInfo.Count; i++)
        {
            HeroSaveData heroSave = new HeroSaveData();
            heroSave.SetInfo(heroInfo[i]);
            herosData.Add(heroSave);
        }

        //string path = Application.persistentDataPath + "/herosave.json";
        //string data = JsonConvert.SerializeObject
        //File.WriteAllText(path, data);
    }

    public void SavePlayerData(Player player)
    {
        string path = Application.persistentDataPath + "/save.json";
        SaveData saveData = MakeSaveData(player);

        string data = JsonUtility.ToJson(saveData, true);
        //string data = JsonConvert.SerializeObject(saveData);
        File.WriteAllText(path, data);
        Debug.Log(data);
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
        {
            Managers.GetPlayer.HeroComp.TakeSavedHero(heroData);
        }

        foreach (var misc in saveData.miscData)
        {
            Managers.GetPlayer.Inven.GainSavedItem(misc);
        }

        foreach (var equip in saveData.equipData)
        {
            Managers.GetPlayer.Inven.GainSavedItem(equip);
        }

        return true;
    }

    SaveData MakeSaveData(Player player)
    {
        SaveData saveData = new SaveData();
        saveData.SetInfo(player);
        return saveData;
    }
}
