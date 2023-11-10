using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDic();
}

public class DataManager
{
    public Dictionary<int, HeroInfo> HeroDict { get; private set; } = new Dictionary<int, HeroInfo>();
    public Dictionary<int, EnemyInfo> EnemyDict { get; private set; } = new Dictionary<int, EnemyInfo>();
    public Dictionary<int, SkillInfo> SkillDict { get; private set; } = new Dictionary<int, SkillInfo>();

    public List<Dictionary<int, StageInfo>> StageDicts { get; private set; } = new List<Dictionary<int, StageInfo>>();
    public Dictionary<int, StageInfo> Stage1Dict { get; private set; } = new Dictionary<int, StageInfo>();


    public void Init()
    {
        HeroDict = LoadJson<HeroInfoData, int, HeroInfo>("HeroInfo").MakeDic();
        EnemyDict = LoadJson<EnemyInfoData, int, EnemyInfo>("EnemyInfo").MakeDic();
        SkillDict = LoadJson<SkillInfoData, int, SkillInfo>("SkillInfo").MakeDic();


        Stage1Dict = LoadJson<StageInfoData, int, StageInfo>("StageInfo1").MakeDic();



        StageDicts.Add(null);
        StageDicts.Add(Stage1Dict);

    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
