using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeroComponent : MonoBehaviour
{
    Dictionary<int, int> heroCount = new Dictionary<int, int>();
    List<int> uniqueHeros = new List<int>();
    List<int> rareHeros = new List<int>();
    List<int> normalHeros = new List<int>();

    public List<int> UniqueHero { get { return uniqueHeros; } }
    public List<int> RareHero { get { return rareHeros; } }
    public List<int> NormalHero { get { return normalHeros; } }
    public Dictionary<int, int> HeroCount { get { return heroCount; } }


    public void TakeNewHero(int _heroId)
    {
        HeroInfo _heroInfo = Managers.Data.HeroDict[_heroId];
        if (heroCount.ContainsKey(_heroId))
            heroCount[_heroId]++;
        else
        {
            heroCount.Add(_heroId, 1);
            if (_heroInfo.grade == (int)Define.HeroGrade.NORMAL)
                normalHeros.Add(_heroId);
            else if (_heroInfo.grade == (int)Define.HeroGrade.RARE)
                rareHeros.Add(_heroId);
            else if (_heroInfo.grade == (int)Define.HeroGrade.UNIQUE)
                uniqueHeros.Add(_heroId);
        }
        
    }

    public void RemoveHero(int _heroId)
    {
        HeroInfo _heroInfo = Managers.Data.HeroDict[_heroId];
        heroCount[_heroId]--;

        if (heroCount[_heroId] == 0)
        {
            heroCount.Remove(_heroId);
            if (_heroInfo.grade == (int)Define.HeroGrade.NORMAL)
                normalHeros.Remove(_heroId);
            else if (_heroInfo.grade == (int)Define.HeroGrade.RARE)
                rareHeros.Remove(_heroId);
            else if (_heroInfo.grade == (int)Define.HeroGrade.UNIQUE)
                uniqueHeros.Remove(_heroId);
        }
      
    }


}
