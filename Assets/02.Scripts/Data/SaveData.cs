using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HeroSaveData
{
    public int level;
    public string creatureName;
    public int id;
    public int maxHp;
    public int maxMp;
    public int attack;
    public int defense;
    public int speed;
    public int role;
    public int grade;

    public int weaponId = -1;
    public int armorId = -1;

    public void SetInfo(Hero _hero)
    {
        level = _hero.Level;
        creatureName = _hero.CreatureName;
        id = _hero.Id;
        maxHp = _hero.MaxHp;
        maxMp = _hero.MaxMp;
        attack = _hero.Attack;
        defense = _hero.Defense;
        speed = _hero.Speed;
        role = _hero.Role;
        grade = _hero.Grade;

        if (_hero.Weapon != null)
            weaponId = _hero.Weapon.Id;
        if(_hero.Armor != null)
            armorId = _hero.Armor.Id;
    }
}

[System.Serializable]
public class ItemSaveData
{
    public int id;
    public string itemName;
    public string description;
    public string type;
    public ItemType itemType;
    public int power;
    public int number;
    public int grade;

    public void SetInfo(Item _item)
    {
        id = _item.Id;
        itemName = _item.ItemName;
        description = _item.Description;
        type = _item.ITypeString;
        itemType = _item.IType;
        power = _item.Power;
        number = _item.Number;
        grade = _item.Grade;
    }
}


[System.Serializable]
public class SaveData
{
    public string playerName;
    public int playerLevel;
    public int gold;
    public int expStone;

    public int chapter;
    public int stage;

    public List<HeroSaveData> herosData = new List<HeroSaveData>();
    public List<ItemSaveData> equipData = new List<ItemSaveData>();
    public List<ItemSaveData> miscData = new List<ItemSaveData>();


    public void SetInfo(Player player)
    {
        playerLevel = 1;
        playerName = Managers.GetPlayer.PlayerName;
        gold = player.Inven.Gold;
        expStone = player.Inven.ExpStone;

        chapter = player.StageComp.OpenedChapter;
        stage = player.StageComp.OpenedStage;

        Debug.Log($"Save Ω√¿€");

        List<Hero> heroInfo = Managers.GetPlayer.HeroComp.Heros;
        List<Item> miscInfo = Managers.GetPlayer.Inven.Items[(int)ItemType.Misc];
        List<Item> equipInfo = Managers.GetPlayer.Inven.Items[(int)ItemType.Equip];

        for (int i = 0; i < heroInfo.Count; i++)
        {
            HeroSaveData heroSave = new HeroSaveData();
            heroSave.SetInfo(heroInfo[i]);
            herosData.Add(heroSave);
        }

        for (int i = 0; i < miscInfo.Count; i++)
        {
            ItemSaveData itemSave = new ItemSaveData();
            itemSave.SetInfo(miscInfo[i]);
            miscData.Add(itemSave);
        }

        for (int i = 0; i < equipInfo.Count; i++)
        {
            ItemSaveData itemSave = new ItemSaveData();
            itemSave.SetInfo(equipInfo[i]);
            equipData.Add(itemSave);
        }
    }
}

