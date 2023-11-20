using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Creature
{
    int skillDamage;
    bool isPicked = false;
    Item weapon;
    Item armor;

    public int SkillDamage { get { return skillDamage; } set { skillDamage = value; } }
    public bool IsPicked { get { return isPicked; } set { isPicked = value; } }
    public Item Weapon { get { return weapon; }}
    public Item Armor { get { return armor; }}

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

        if (Managers.Data.SkillDict.ContainsKey(Id) == false) return;
        skillDamage = Managers.Data.SkillDict[Id].lv1;
    }
    public void LevelUp()
    {
        level++;

        maxHp += (int)(maxHp * 0.1f);
        maxMp += (int)(maxMp * 0.1f);
        attack += (int)(attack * 0.2f);
        defense += (int)(defense * 0.1f);

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
    }


    public void EquipItem(Item _item)
    {
        if(_item.ITypeString == "Armor")
        {
            if (armor != null) Managers.GetPlayer.Inven.GainItem(armor);
            armor = _item;
            // ¹æ¾î·Â »ó½Â
        }
        else
        {
            if (weapon != null) Managers.GetPlayer.Inven.GainItem(weapon);
            weapon = _item;
            // °ø°Ý·Â »ó½Â
        }

        Managers.GetPlayer.Inven.RemoveItem(_item);
    }

    public void UnEquipItem(string _type)
    {
        if (_type == "Armor")
        {
            Managers.GetPlayer.Inven.GainItem(armor);
            armor = null;
        }
        else
        {
            Managers.GetPlayer.Inven.GainItem(weapon);
            weapon = null;
        } 
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
        }

        return color;
    }
}
