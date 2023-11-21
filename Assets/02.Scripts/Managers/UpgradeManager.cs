using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager
{
    Hero mainHero;
    List<Hero> ingredientHeros = new List<Hero>();

    public Hero MainHero { get { return mainHero; }  set { mainHero = value; } }

    public bool Upgrade()
    {
        if (mainHero == null)
            return false;

        int ingredientNumber = Managers.Data.UpgradeDict[mainHero.Grade].sameHero + Managers.Data.UpgradeDict[mainHero.Grade].sameGrade;

        if (ingredientHeros.Count < ingredientNumber)
            return false;

        for (int i = 0; i < ingredientHeros.Count; i++)
        {
            ingredientHeros[i].UnEquipAllItems();
            Managers.GetPlayer.HeroComp.Heros.Remove(ingredientHeros[i]);
        }

        mainHero.UpGrade();
        return true;
    }

    public void RegisterIngredient(Hero _hero)
    {
        ingredientHeros.Add(_hero);
    }

    public void RemoveIngredient(Hero _hero)
    {
        ingredientHeros.Remove(_hero);
    }

    public bool IsRegisteredHero(Hero _hero)
    {
        return ingredientHeros.Contains(_hero);
    }

    public void Clear()
    {
        mainHero = null;
        ingredientHeros.Clear();
    }
}
