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
        Debug.Log("��Ʋ ����");
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
        // 1. ������ Creature �̾ƿ� �� Ÿ�� ����
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

            // 2. ������ Creature�� ���ݱ��� ���� �ð���ŭ ���� �ֵ� �ð� ����
            for (int i = 0; i < order.Count; i++)
                DecreaseAttackGage(i, remainToAttack);

            // 3. �����ڰ� Ÿ�� �����ϰ� ��
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
            // 1. ���� �ֱ�(x)
            // 2. Chpater, Stage ����(o)
            Debug.Log($"Opened : {Managers.GetPlayer.StageComp.OpenedChapter}-{Managers.GetPlayer.StageComp.OpenedStage}, Now : {NowChapter} - {NowStage}");
            if (Managers.GetPlayer.StageComp.OpenedChapter != NowChapter)
                return;
            if (Managers.GetPlayer.StageComp.OpenedStage != NowStage)
                return;

            Managers.GetPlayer.StageComp.OpenedStage++;

                // ������ é�Ͷ�� ���� é�� �ع�(x)
        }

        // 3. UI���� - win ���� ���� �ؽ�Ʈ ����(o)
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
