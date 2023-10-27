using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
