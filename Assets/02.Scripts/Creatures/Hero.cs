using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Creature
{
    int skillDamage;
    bool isPicked = false;

    public int SkillDamage { get { return skillDamage; } set { skillDamage = value; } }
    public bool IsPicked { get { return isPicked; } set { isPicked = value; } }

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

        skillDamage = Managers.Data.SkillDict[Id].lv1;
    }
    public void LevelUp()
    {
        level++;

        maxHp += (int)(maxHp * 0.1f);
        maxMp += (int)(maxMp * 0.1f);
        attack += (int)(attack * 0.2f);
        defense += (int)(defense * 0.1f);

        if(level == 5)
            skillDamage = Managers.Data.SkillDict[Id].lv2;

        if(level == 10)
            skillDamage = Managers.Data.SkillDict[Id].lv3;
    }
    public void EquipItem(Item _item)
    {
        // 여기서 스탯 추가
    }
}
