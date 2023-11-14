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
            return y.Grade.CompareTo(x.Grade);
        else
            return y.Level.CompareTo(x.Level);
    }
}


public class HeroComponent : MonoBehaviour
{
    const int HERO_NONE = -1;

    Dictionary<int, int> heroCount = new Dictionary<int, int>();

    List<Hero> heros = new List<Hero>();

    public List<Hero> Heros { get { return heros; } }
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
        heros.Add(hero);
    }
    public void RemoveHero(Hero _hero)
    {
        heros.Remove(_hero);
    }
    public void Sort()
    {
        heros.Sort(new HeroCompare());
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
                //�̹� �ߺ��� �����Դϴ�.
                Debug.Log("�ش� ������ �̹� ��ġ�Ǿ����ϴ�.");
                return -1;
            }
        }
        int setPlace = FindEmptyPlace();

        if (setPlace == -1)
        {
            // ������ �� á���ϴ�.
            Debug.Log("������ �� á���ϴ�.");
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
