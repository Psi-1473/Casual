using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonManager
{
    List<int> normals = new List<int>() { 3, 4, 5, 10, 11, 12, 16, 17, 21, 22};
    List<int> rares = new List<int>() { 1, 2, 8, 9, 14, 15, 19};
    List<int> uniques = new List<int>() { 0, 6, 7, 13, 18, };

    public void NormalSummon()
    {
        int value = Random.Range(1, 100);
        int idx;
        int heroId;
        if(value <= 50)
        {
            idx = Random.Range(0, normals.Count);
            heroId = normals[idx];
        }
        else
        {
            idx = Random.Range(0, rares.Count);
            heroId = rares[idx];
        }


        Managers.GetPlayer.HeroComp.TakeNewHero(heroId);
        Debug.Log($"Get Hero! : {Managers.Data.HeroDict[heroId].name}");
    }

    public void RareSummon()
    {
        int value = Random.Range(1, 100);
        int idx;
        int heroId;
        if (value <= 50)
        {
            idx = Random.Range(0, uniques.Count);
            heroId = uniques[idx];
        }
        else
        {
            idx = Random.Range(0, rares.Count);
            heroId = rares[idx];
        }


        Managers.GetPlayer.HeroComp.TakeNewHero(heroId);
        Debug.Log($"Get Hero! : {Managers.Data.HeroDict[heroId].name}");
    }




}
