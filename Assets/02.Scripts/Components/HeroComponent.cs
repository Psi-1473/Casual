using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCompare : IComparer<Hero>
{
    public int Compare(Hero x, Hero y)
    {
        if (x.Level == y.Level && x.Grade == y.Grade)
            return x.Id.CompareTo(y.Id);
        else if(x.Level == y.Level && x.Grade != y.Grade)
            return x.Grade.CompareTo(y.Grade);
        else
            return x.Level.CompareTo(y.Level);
    }
}


public class HeroComponent : MonoBehaviour
{
    const int HERO_NONE = -1;

    Dictionary<int, int> heroCount = new Dictionary<int, int>();
    List<Hero> uniqueHeros = new List<Hero>();
    List<Hero> rareHeros = new List<Hero>();
    List<Hero> normalHeros = new List<Hero>();

    public List<Hero> UniqueHero { get { return uniqueHeros; } }
    public List<Hero> RareHero { get { return rareHeros; } }
    public List<Hero> NormalHero { get { return normalHeros; } }
    public Dictionary<int, int> HeroCount { get { return heroCount; } }

    [SerializeField]
    public Dictionary<int, Hero> HeroFormation { get; private set; } = new Dictionary<int, Hero>();

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        HeroFormation.Add(1, null);
        HeroFormation.Add(2, null);
        HeroFormation.Add(3, null);
        HeroFormation.Add(4, null);
        HeroFormation.Add(5, null);
    }

    public void TakeNewHero(int _heroId)
    {
        Hero hero = new Hero();
        hero.SetNewCreatureInfo(_heroId);

        int grade = hero.Grade;

        if (grade == (int)Define.HeroGrade.NORMAL)
            normalHeros.Add(hero);
        else if (grade == (int)Define.HeroGrade.RARE)
            rareHeros.Add(hero);
        else if (grade == (int)Define.HeroGrade.UNIQUE)
                uniqueHeros.Add(hero);
        
    }
    public void RemoveHero(Hero _hero)
    {
        int grade = _hero.Grade;

        if (grade == (int)Define.HeroGrade.NORMAL)
            normalHeros.Remove(_hero);
        else if (grade == (int)Define.HeroGrade.RARE)
            rareHeros.Remove(_hero);
        else if (grade == (int)Define.HeroGrade.UNIQUE)
            uniqueHeros.Remove(_hero); 
    }
    public void Sort()
    {
        uniqueHeros.Sort(new HeroCompare());
        rareHeros.Sort(new HeroCompare());
        normalHeros.Sort(new HeroCompare());
    }

    public void SetOffHeroFormation(int _place)
    {
        HeroFormation[_place] = null;
    }
    public int SetHeroFormation(Hero _hero)
    {
        for(int i = 1; i < 6; i++)
        {
            if (HeroFormation[i] != null && HeroFormation[i].Id == _hero.Id)  
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
            HeroFormation[setPlace] = _hero;
            return setPlace;
        }
    }

    
    public int FindEmptyPlace()
    {
        for(int i = 1; i < 6; i++)
        {
            if (HeroFormation[i] == null)
                return i;
        }

        return -1;
    }
}
