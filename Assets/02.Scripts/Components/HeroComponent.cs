using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCompare : IComparer<Hero>
{
    public int Compare(Hero x, Hero y)
    {
        if(x.IsPicked == y.IsPicked && x.Level == y.Level && x.Grade == y.Grade)
            return x.Id.CompareTo(y.Id);
        if (x.IsPicked == y.IsPicked && x.Level == y.Level && x.Grade != y.Grade)
            return y.Grade.CompareTo(x.Grade);
        else if(x.IsPicked == y.IsPicked && x.Level != y.Level)
            return y.Level.CompareTo(x.Level);
        else
            return y.IsPicked.CompareTo(x.IsPicked);
    }
}


public class HeroComponent : MonoBehaviour
{
    const int HERO_NONE = -1;

    List<Hero> heros = new List<Hero>();
    public List<Hero> Heros { get { return heros; } }

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

    public void TakeSavedHero(HeroSaveData _heroData)
    {
        Hero hero = new Hero();
        hero.SetCreatureBySaveData(_heroData);
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
        HeroFormation[_place].IsPicked = false;
        HeroFormation[_place] = null;
    }
    public int SetHeroFormation(Hero _hero)
    {
        for(int i = 1; i < 6; i++)
        {
            if (HeroFormation[i] != null && HeroFormation[i].Id == _hero.Id)  
            {
                Debug.Log("해당 영웅이 이미 배치되었습니다.");
                return -1;
            }
        }
        int setPlace = FindEmptyPlace();

        if (setPlace == -1)
        {
            Debug.Log("진영이 꽉 찼습니다.");
            return -1;
        }
        else
        {
            HeroFormation[setPlace] = _hero;
            _hero.IsPicked = true;
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
