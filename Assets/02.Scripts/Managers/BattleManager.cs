using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderInfo
{
    public Creature creature;
    public int remainToAttack;
}

public class BattleManager : MonoBehaviour
{
    const int MAX_GAGE = 10;
    const int CREATURE_NONE = -1;

    List<GameObject> heros = new List<GameObject>();
    List<GameObject> enemies = new List<GameObject>();

    List<OrderInfo> order = new List<OrderInfo>();
    

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
        // 1. 공격할 Creature 뽑아온 뒤 타겟 설정
        OrderInfo info = PopOrder();
        Creature attacker = info.creature;
        Creature target = FindTarget(attacker);
        int remainToAttack = info.remainToAttack;

        // 2. 공격할 Creature의 공격까지 남은 시간만큼 남은 애들 시간 차감
        for(int i = 0; i < order.Count; i++)
            DecreaseAttackGage(i, remainToAttack);

        // 3. 공격자가 타겟 공격하게 함
        attacker.Attack(target);
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
        order[idx].remainToAttack -= value;

        if (order[idx].remainToAttack < 0)
            order[idx].remainToAttack = 0;
    }
    public void PushOrder(Creature _creature)
    {
        OrderInfo info = new OrderInfo();
        info.creature = _creature;
        info.remainToAttack = MAX_GAGE - _creature.Stat.Speed;

        order.Add(info);

        int now = order.Count - 1;

        while (now > 0)
        {
            int next = (now - 1) / 2;
            if (order[now].remainToAttack >= order[next].remainToAttack)
                break;

            OrderInfo tempInfo = info;
            order[now] = order[next];
            order[next] = tempInfo;

            now = next;
        }

        Debug.Log($"Push Creature! : {_creature.name}, {MAX_GAGE - _creature.Stat.Speed}");
    }

    OrderInfo PopOrder()
    {
        OrderInfo retInfo = order[0];
        int lastIndex = order.Count - 1;
        order[0] = order[lastIndex];
        order.RemoveAt(lastIndex);
        lastIndex--;

        Heapify(lastIndex);

        return retInfo;
    }

    void Heapify(int _lastIndex)
    {
        int now = 0;
        while (true)
        {
            int left = 2 * now + 1;
            int right = 2 * now + 2;

            int next = now;
            if (left <= _lastIndex && order[next].remainToAttack >= order[left].remainToAttack)
                next = left;

            if (right <= _lastIndex && order[next].remainToAttack >= order[right].remainToAttack)
                next = right;

            if (next == now)
                break;

            OrderInfo tempInfo = order[now];
            order[now] = order[next];
            order[next] = tempInfo;

            now = next;
        }
    }

}
