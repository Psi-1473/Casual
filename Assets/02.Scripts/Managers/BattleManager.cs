using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderInfo
{
    public AIController creatureAI;
    public int remainToAttack;
}

public class BattleManager : MonoBehaviour
{
    const int MAX_GAGE = 10;
    const int CREATURE_NONE = -1;

    int enemyNum = 0;
    int heroNum = 0;

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
        heroNum = 0;
        enemyNum = 0;

        for(int i = 0; i < 6; i++)
        {
            if (heros[i] != null)
            {
                PushOrder(heros[i].GetComponent<AIController>());
                heroNum++;
            }
        }

        for (int i = 0; i < 6; i++)
        {
            if (enemies[i] != null)
            {
                PushOrder(enemies[i].GetComponent<AIController>());
                enemyNum++;
            }
        }

        ProceedPhase();
    }

    public void ProceedPhase()
    {
        Debug.Log("Proceed Phase ! ");
        // 1. 공격할 Creature 뽑아온 뒤 타겟 설정
        if (heroNum == 0 || enemyNum == 0)
        {
            EndBattle();
            Debug.Log("END Battle");
        }
        else
        {

            OrderInfo info = PopOrder();
            AIController attacker = info.creatureAI;

            if (attacker.IsDead)
            {
                ProceedPhase();
                return;
            }

            AIController target = FindTarget(attacker);
            int remainToAttack = info.remainToAttack;

            // 2. 공격할 Creature의 공격까지 남은 시간만큼 남은 애들 시간 차감
            for (int i = 0; i < order.Count; i++)
                DecreaseAttackGage(i, remainToAttack);

            // 3. 공격자가 타겟 공격하게 함
            attacker.SetStateAttack(target);
        }
    }

    public void RemoveCreature(GameObject obj, int formationNum)
    {
        
        if (obj.GetComponent<AIController>().CType == CreatureType.CREATURE_HERO)
        {
            heros[formationNum] = null;
            heroNum--;

            Debug.Log($" Left Hero : {heroNum}");
        }

        if (obj.GetComponent<AIController>().CType == CreatureType.CREATURE_ENEMY)
        {
            enemies[formationNum] = null;
            enemyNum--;

            Debug.Log($" Left Enemy : {enemyNum}");
        }
       //Creature creature = obj.GetComponent<Creature>();
       //int idx = order.FindIndex((x) => x.creature.Equals(creature));
       //order.RemoveAt(idx);

    }

    public void EndBattle()
    {
        bool win = false;
        if (enemyNum == 0) win = true;

        order.Clear();

        if(win)
        {
            // 1. 보상 주기(x)
            // 2. Chpater, Stage 개방(o)
            Debug.Log($"Opened : {Managers.GetPlayer.StageComp.OpenedChapter}-{Managers.GetPlayer.StageComp.OpenedStage}, Now : {NowChapter} - {NowStage}");
            if (Managers.GetPlayer.StageComp.OpenedChapter != NowChapter)
                return;
            if (Managers.GetPlayer.StageComp.OpenedStage != NowStage)
                return;

            Managers.GetPlayer.StageComp.OpenedStage++;

                // 마지막 챕터라면 다음 챕터 해방(x)
        }

        // 3. UI띄우기 - win 값에 따라 텍스트 세팅(o)
        UI_BattleEnd _ui = Managers.UI.ShowPopupUI<UI_BattleEnd>();
        _ui.SetText(win);
    }

    AIController FindTarget(AIController _pawn)
    {
        AIController target = null;
        if (_pawn.CType == CreatureType.CREATURE_HERO)
        {
            for (int i = 0; i < 6; i++)
            {
                if (enemies[i] != null)
                {
                    target = enemies[i].GetComponent<AIController>();
                    break;
                }
            }
        }
        else if (_pawn.CType == CreatureType.CREATURE_ENEMY)
        {
            for (int i = 0; i < 6; i++)
            {
                if (heros[i] != null)
                {
                    target = heros[i].GetComponent<AIController>();
                    break;
                }
            }
        }

        return target;
    }

    void DecreaseAttackGage(int _idx, int _value)
    {
        order[_idx].remainToAttack -= _value;

        if (order[_idx].remainToAttack < 0)
            order[_idx].remainToAttack = 0;
    }
    public void PushOrder(AIController _pawn)
    {
        OrderInfo info = new OrderInfo();
        info.creatureAI = _pawn;
        info.remainToAttack = MAX_GAGE - _pawn.Stat.Speed;

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

        Debug.Log($"Push Creature! : {_pawn.name}, {MAX_GAGE - _pawn.Stat.Speed}");
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
