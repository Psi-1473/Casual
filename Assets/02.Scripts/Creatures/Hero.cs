using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Creature
{

    int maxGrade;
    int skillDamage;
    bool isPicked = false;
    Item weapon;
    Item armor;

    public int SkillDamage { get { return skillDamage; } set { skillDamage = value; } }
    public int MaxGrade { get { return maxGrade; } set { maxGrade = value; } }
    public bool IsPicked { get { return isPicked; } set { isPicked = value; } }
    public Item Weapon { get { return weapon; }}
    public Item Armor { get { return armor; }}

    public Action OnEquipChanged;

    public override void SetNewCreatureInfo(int Id)
    {
        HeroInfo heroInfo = Managers.Data.HeroDict[Id];

        level = 1;
        creatureName = heroInfo.name;
        id = heroInfo.id;
        maxHp = heroInfo.hp;
        maxMp = heroInfo.mp ;
        attack = heroInfo.attack;
        defense = heroInfo.defense;
        speed = heroInfo.speed;
        role = heroInfo.role;
        grade = heroInfo.grade;
        SetMaxGrade(grade);

        if (Managers.Data.SkillDict.ContainsKey(Id) == false) return;
        
        skillDamage = Managers.Data.SkillDict[Id].lv1;
        buffCode = Managers.Data.SkillDict[Id].buffType;
    }

    public void SetCreatureBySaveData(HeroSaveData _saveData)
    {
        level = _saveData.level;
        creatureName = _saveData.creatureName;
        id = _saveData.id;
        maxHp = _saveData.maxHp;
        maxMp = _saveData.maxMp;
        attack = _saveData.attack;
        defense = _saveData.defense;
        speed = _saveData.speed;
        role = _saveData.role;
        grade = _saveData.grade;

        if (Managers.Data.SkillDict.ContainsKey(Id) == false) return;

        skillDamage = Managers.Data.SkillDict[Id].lv1;
        buffCode = Managers.Data.SkillDict[Id].buffType;

        if (level == 5)
            skillDamage = Managers.Data.SkillDict[Id].lv2;

        if (level == 10)
            skillDamage = Managers.Data.SkillDict[Id].lv3;

        if(_saveData.weaponId != -1)
        {
            ItemInfo savedWeapon = Managers.Data.EquipDict[_saveData.weaponId];
            if (savedWeapon != null)
            {
                Item item = new Item();
                item.SetInfo(savedWeapon, 1);
                weapon = item;
            }
        }

        if (_saveData.armorId != -1)
        {
            ItemInfo savedArmor = Managers.Data.EquipDict[_saveData.armorId];
            if (savedArmor != null)
            {
                Item item = new Item();
                item.SetInfo(savedArmor, 1);
                armor = item;
            }
        }



    }
    public void LevelUp()
    {
        HeroInfo heroInfo = Managers.Data.HeroDict[Id];
        level++;

        maxHp += (int)(heroInfo.hp * 0.1f);
        attack += (int)(heroInfo.attack * 0.2f);
        defense += (int)(heroInfo.defense * 0.1f);

        if (Managers.Data.SkillDict.ContainsKey(Id) == false) return;

        if (level == 5)
            skillDamage = Managers.Data.SkillDict[Id].lv2;

        if(level == 10)
            skillDamage = Managers.Data.SkillDict[Id].lv3;
    }

    public void UpGrade()
    {
        Debug.Log("Hero Upgrade");
        grade++;
        HeroInfo heroInfo = Managers.Data.HeroDict[Id];
        maxHp += (int)(heroInfo.hp * 0.5f);
        attack += (int)(heroInfo.attack * 0.5f);
        defense += (int)(heroInfo.defense * 0.5f);
    }
    public void EquipItem(Item _item)
    {
        if(_item.ITypeString == "Armor")
        {
            if (armor != null) Managers.GetPlayer.Inven.GainItem(armor);
            armor = _item;
            defense += _item.Power;
        }
        else
        {
            if (weapon != null) Managers.GetPlayer.Inven.GainItem(weapon);
            weapon = _item;
            attack += _item.Power;
        }

        Managers.GetPlayer.Inven.RemoveItem(_item);
        if (OnEquipChanged != null) OnEquipChanged.Invoke();
    }
    public void UnEquipItem(string _type)
    {
        if (_type == "Armor")
        {
            if (armor != null)
            {
                Managers.GetPlayer.Inven.GainItem(armor);
                defense -= armor.Power;
            }
            armor = null;
        }
        else
        {
            if (weapon != null)
            {
                Managers.GetPlayer.Inven.GainItem(weapon);
                attack -= weapon.Power;
            }
            weapon = null;
        }

        if (OnEquipChanged != null) OnEquipChanged.Invoke();
    }
    public void UnEquipAllItems()
    {
        UnEquipItem("Armor");
        UnEquipItem("Weapon");
    }

    public Color GetStarColor()
    {
        int _grade = grade / 3;

        Color color = new Color();
        color.a = 1f;
        switch (_grade)
        {
            case 0:
                color.r = 1f;
                color.g = 1f;
                color.b = 1f;
                break;
            case 1:
                color.r = 0f;
                color.g = 1f;
                color.b = 0f;
                break;
            case 2:
                color.r = 1f;
                color.g = 0.3f;
                color.b = 0.3f;
                break;
            case 3:
                color.r = 1f;
                color.g = 1f;
                color.b = 0f;
                break;
        }

        return color;
    }

    void SetMaxGrade(int startGrade)
    {
        switch(startGrade)
        {
            case 0:
                maxGrade = 5;
                break;
            case 3:
                maxGrade = 8;
                break;
            case 6:
                maxGrade = 9;
                break;
        }
    }
}
