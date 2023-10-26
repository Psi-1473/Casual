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


    public void Init()
    {
        HeroDict = LoadJson<HeroInfoData, int, HeroInfo>("HeroInfo").MakeDic();

    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
