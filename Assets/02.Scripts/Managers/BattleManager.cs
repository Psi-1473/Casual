using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderInfo
{
    public Creature creature;
    public int attackGage;
}

public class BattleManager : MonoBehaviour
{
    const int MAX_GAGE = 10;
    const int CREATURE_NONE = -1;

    List<GameObject> heros = new List<GameObject>();
    List<GameObject> enemies = new List<GameObject>();

    List<Tuple<Creature, int>> order = new List<Tuple<Creature, int>>();
    

    public List<GameObject> Heros { get { return heros; }  }
    public List<GameObject> Enemies { get { return enemies; } }



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


    public void BeginBattle()
    {
        Debug.Log("배틀 시작");

        for(int i = 0; i < 6; i++)
        {
            if (heros[i] != null)
                PushOrder(heros[i].GetComponent<Creature>());
        }

        for (int i = 0; i < 6; i++)
        {
            if (enemies[i] != null)
                PushOrder(enemies[i].GetComponent<Creature>());
        }
    }

    public void ProceedPhase()
    {
        Tuple<Creature, int> tuple = PopOrder();
        Creature target = FindTarget(tuple.Item1);
        int attackerGage = tuple.Item2;


        for(int i = 0; i < order.Count; i++)


    }

    Creature FindTarget(Creature _creature)
    {
        Creature target = null;
        if (_creature.GetComponent<Hero>() != null)
        {
            for (int i = 0; i < 6; i++)
            {
                if (enemies[i] != null)
                {
                    target = enemies[i].GetComponent<Creature>();
                    break;
                }
            }
        }
        else if (_creature.GetComponent<Enemy>() != null)
        {
            for (int i = 0; i < 6; i++)
            {
                if (heros[i] != null)
                {
                    target = heros[i].GetComponent<Creature>();
                    break;
                }
            }
        }

        return target;
    }

    void DecreaseAttackGage(int idx, int value)
    {
  
    }
    public void PushOrder(Creature _creature)
    {
        Tuple<Creature, int> tuple = new Tuple<Creature, int>(_creature, MAX_GAGE - _creature.Stat.Speed);

        order.Add(tuple);

        int now = order.Count - 1;

        while (now > 0)
        {
            int next = (now - 1) / 2;
            if (order[now].Item2 >= order[next].Item2)
                break;

            Tuple<Creature, int> tempTuple = tuple;
            order[now] = order[next];
            order[next] = tempTuple;

            now = next;
        }

        Debug.Log($"Push Creature! : {_creature.name}, {MAX_GAGE - _creature.Stat.Speed}");
    }

    Tuple<Creature, int> PopOrder()
    {
        Tuple<Creature, int> retTuple = order[0];
        int lastIndex = order.Count - 1;
        order[0] = order[lastIndex];
        order.RemoveAt(lastIndex);
        lastIndex--;

        Heapify(lastIndex);

        return retTuple;
    }

    void Heapify(int _lastIndex)
    {
        int now = 0;
        while (true)
        {
            int left = 2 * now + 1;
            int right = 2 * now + 2;

            int next = now;
            if (left <= _lastIndex && order[next].Item2 >= order[left].Item2)
                next = left;

            if (right <= _lastIndex && order[next].Item2 >= order[right].Item2)
                next = right;

            if (next == now)
                break;

            Tuple<Creature, int> tempTuple = order[now];
            order[now] = order[next];
            order[next] = tempTuple;

            now = next;
        }
    }

}
