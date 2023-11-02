using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HeroComponent : MonoBehaviour
{
    const int HERO_NONE = -1;

    Dictionary<int, int> heroCount = new Dictionary<int, int>();
    List<int> uniqueHeros = new List<int>();
    List<int> rareHeros = new List<int>();
    List<int> normalHeros = new List<int>();

    public List<int> UniqueHero { get { return uniqueHeros; } }
    public List<int> RareHero { get { return rareHeros; } }
    public List<int> NormalHero { get { return normalHeros; } }
    public Dictionary<int, int> HeroCount { get { return heroCount; } }

    [SerializeField]
    public Dictionary<int, int> HeroFormation { get; private set; } = new Dictionary<int, int>();

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        HeroFormation.Add(1, HERO_NONE);
        HeroFormation.Add(2, HERO_NONE);
        HeroFormation.Add(3, HERO_NONE);
        HeroFormation.Add(4, HERO_NONE);
        HeroFormation.Add(5, HERO_NONE);
    }

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
    
    public void SetOffHeroFormation(int _place)
    {
        HeroFormation[_place] = -1;
    }
    public int SetHeroFormation(int _heroId)
    {
        for(int i = 1; i < 6; i++)
        {
            if (HeroFormation[i] == _heroId)
            {
                //이미 중복된 영웅입니다.
                Debug.Log("해당 영웅이 이미 배치되었습니다.");
                return -1;
            }
        }
        int setPlace = FindEmptyPlace();

        if (setPlace == -1)
        {
            // 진영이 곽 찼습니다.
            Debug.Log("진영이 꽉 찼습니다.");
            return -1;
        }
        else
        {
            HeroFormation[setPlace] = _heroId;
            return setPlace;
        }
    }

    
    public int FindEmptyPlace()
    {
        for(int i = 1; i < 6; i++)
        {
            if (HeroFormation[i] == HERO_NONE)
                return i;
        }

        return -1;
    }
}
