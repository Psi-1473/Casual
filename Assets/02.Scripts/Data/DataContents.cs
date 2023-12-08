using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region HeroInfo
[Serializable]
public class HeroInfo
{
    public int id;
    public int grade;
    public string name;
    public string info;
    public int attack;
    public int defense;
    public int hp;
    public int mp;
    public int role;
    public int speed;
}

[Serializable]
public class HeroInfoData : ILoader<int, HeroInfo>
{
    public List<HeroInfo> heros = new List<HeroInfo>();

    public Dictionary<int, HeroInfo> MakeDic()
    {
        Dictionary<int, HeroInfo> dict = new Dictionary<int, HeroInfo>();
        foreach (HeroInfo hero in heros)
            dict.Add(hero.id, hero);
        return dict;
    }
}
#endregion

#region EnemyInfo
[Serializable]
public class EnemyInfo
{
    public int id;
    public string name;
    public int attack;
    public int defense;
    public int hp;
    public int mp;
    public int speed;
    public int role;
    public int grade;
}

[Serializable]
public class EnemyInfoData : ILoader<int, EnemyInfo>
{
    public List<EnemyInfo> enemies = new List<EnemyInfo>();

    public Dictionary<int, EnemyInfo> MakeDic()
    {
        Dictionary<int, EnemyInfo> dict = new Dictionary<int, EnemyInfo>();
        foreach (EnemyInfo enemy in enemies)
            dict.Add(enemy.id, enemy);
        return dict;
    }
}
#endregion

#region SkillInfo
[Serializable]
public class SkillInfo
{
    public int id;
    public string name;
    public string description;
    public int lv1;
    public int lv2;
    public int lv3;
    public int statEffect;
    public int skillType;
    public string buffType;
}

[Serializable]
public class SkillInfoData : ILoader<int, SkillInfo>
{
    public List<SkillInfo> skills = new List<SkillInfo>();

    public Dictionary<int, SkillInfo> MakeDic()
    {
        Dictionary<int, SkillInfo> dict = new Dictionary<int, SkillInfo>();
        foreach (SkillInfo skill in skills)
            dict.Add(skill.id, skill);
        return dict;
    }
}
#endregion

#region ItemInfo
[Serializable]
public class ItemInfo
{
    public int id;
    public string name;
    public string description;
    public int power;
    public string itemType;
    public int grade;
    public int price;
}

[Serializable]
public class ItemInfoData : ILoader<int, ItemInfo>
{
    public List<ItemInfo> items = new List<ItemInfo>();

    public Dictionary<int, ItemInfo> MakeDic()
    {
        Dictionary<int, ItemInfo> dict = new Dictionary<int, ItemInfo>();
        foreach (ItemInfo item in items)
            dict.Add(item.id, item);
        return dict;
    }
}
#endregion
#region StageInfo
[Serializable]
public class StageInfo
{
    public int id;
    public int frontTop;
    public int frontBottom;
    public int backTop;
    public int backMiddle;
    public int backBottom;
    public int heroExp;
    public int playerExp;
    public int gold;
}

[Serializable]
public class StageInfoData : ILoader<int, StageInfo>
{
    public List<StageInfo> stages = new List<StageInfo>();

    public Dictionary<int, StageInfo> MakeDic()
    {
        Dictionary<int, StageInfo> dict = new Dictionary<int, StageInfo>();
        foreach (StageInfo stage in stages)
            dict.Add(stage.id, stage);
        return dict;
    }
}
#endregion
#region ExpInfo
[Serializable]
public class ExpInfo
{
    public int level;
    public int exp;
}

[Serializable]
public class ExpInfoData : ILoader<int, ExpInfo>
{
    public List<ExpInfo> exp = new List<ExpInfo>();

    public Dictionary<int, ExpInfo> MakeDic()
    {
        Dictionary<int, ExpInfo> dict = new Dictionary<int, ExpInfo>();
        foreach (ExpInfo value in exp)
            dict.Add(value.level, value);
        return dict;
    }
}

#endregion
#region UpgradeInfo
[Serializable]
public class UpgradeInfo
{
    public int grade;
    public int sameHero;
    public int sameGrade;
}

[Serializable]
public class UpgradeInfoData : ILoader<int, UpgradeInfo>
{
    public List<UpgradeInfo> info = new List<UpgradeInfo>();

    public Dictionary<int, UpgradeInfo> MakeDic()
    {
        Dictionary<int, UpgradeInfo> dict = new Dictionary<int, UpgradeInfo>();
        foreach (UpgradeInfo value in info)
            dict.Add(value.grade, value);
        return dict;
    }
}
#endregion