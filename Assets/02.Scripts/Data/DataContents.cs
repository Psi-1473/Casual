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

