using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager
{
    List<GameObject> heros = new List<GameObject>();
    List<GameObject> enemies = new List<GameObject>();

    public int NowChapter { get; set; }
    public int NowStage { get; set; }

    public void Init()
    {
        heros.Add(null);
        heros.Add(null);
        heros.Add(null);
        heros.Add(null);
        heros.Add(null);
        heros.Add(null);

        enemies.Add(null);
        enemies.Add(null);
        enemies.Add(null);
        enemies.Add(null);
        enemies.Add(null);
        enemies.Add(null);
    }
    
    public void SetHero(GameObject _hero, int _place)
    {
        heros[_place] = _hero;
    }
    public void SetEnemy(GameObject _enemy, int _place)
    {
        enemies[_place] = _enemy;
    }


    public void Clear()
    {
        for (int i = 0; i < 6; i++)
            heros[i] = null;

        for (int i = 0; i < 6; i++)
            enemies[i] = null;
    }
}
