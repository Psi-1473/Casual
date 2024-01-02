using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderInfo
{
    public AIController creatureAI;
    public int remainToAttack;
}

public class BattleManager
{
    const int MAX_GAGE = 10;
    const int MAX_CREATURE = 5;

    int aliveEnemies = 0;
    int aliveHeros = 0;

    List<OrderInfo> order = new List<OrderInfo>();
    List<GameObject> heros = new List<GameObject>();
    List<GameObject> enemies = new List<GameObject>();

    public List<GameObject> Heros { get { return heros; }  }
    public List<GameObject> Enemies { get { return enemies; } }

    public int NowChapter { get; set; }
    public int NowStage { get; set; }

    public void Init()
    {
        /* 
         * hero, enemy ��� �ִ� ��ü ���� 5, �迭�� 1~5 index�� ��� �����̹Ƿ� �̸� null�� �ʱ�ȭ
         * ���� AI ��ü�� ������ ���� �ڱ� �ڸ��� �������ֱ� ������ �ڸ��� ���缭 ��ü�� ��ġ�ϱ� ���� �̸� 1~5�� index�� ����� �� �ֵ��� �ʱ�ȭ
         * ex) hero[1]�� �ִ� ��ü�� hero ���� 1�� �ڸ��� ��ġ�� ��ü
         */
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
    public void Clear()
    {
        for (int i = 0; i <= MAX_CREATURE; i++)
            heros[i] = null;

        for (int i = 0; i <= MAX_CREATURE; i++)
            enemies[i] = null;
    }


    public void BeginBattle()
    {
        /* 
         * ������ ���۵Ǿ� ���� ������ ���̵��� �Ͼ�� ����
         * ���� ���� ��, �ܺο��� heros�� enemies ���� �����ϸ� ��ü�� ���� ������ �����ϴ� ����Ʈ�� ������ �� �� ����
         */
        aliveHeros = 0;
        aliveEnemies = 0;

        for(int i = 0; i <= MAX_CREATURE; i++)
        {
            if (heros[i] != null)
            {
                PushOrder(heros[i].GetComponent<AIController>());
                aliveHeros++;
            }
        }

        for (int i = 0; i <= MAX_CREATURE; i++)
        {
            if (enemies[i] != null)
            {
                PushOrder(enemies[i].GetComponent<AIController>());
                aliveEnemies++;
            }
        }

        ProceedPhase();
    }
    public void ProceedPhase()
    {
        /*
         * �� ���� �����ϴ� �Լ�
         * ���� ���� ť���� ���� ������ ��ü�� �̾� ���� Ÿ���� ã���ְ� ������ ��ü���� ���� ���� ������ ��, ť���� ���� ��ü�� ���� ����
         * �� �� ������ ���� ��ü ���� 0�̵Ǹ� ���� ����
         */
        if (aliveHeros == 0 || aliveEnemies == 0)
            EndBattle();
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
            int decreaseValue = info.remainToAttack;

            for (int i = 0; i < order.Count; i++)
                DecreaseAttackGage(i, decreaseValue);

            attacker.BeginTurn(target);
        }
    }
    public void EndBattle()
    {
        /*
         * ������ ��/�и� �Ǵ��ϰ� ���� ����
         */
        bool win = (aliveEnemies == 0);

        order.Clear();

        int _rewardGold = 0;
        int _expStone = 0;
        if (win)
        {
            _rewardGold = Managers.Data.StageDicts[NowChapter][NowStage].gold;
            _expStone = Managers.Data.StageDicts[NowChapter][NowStage].playerExp;
            Managers.GetPlayer.Inven.Gold += _rewardGold;
            Managers.GetPlayer.Inven.ExpStone += _expStone;

            Managers.GetPlayer.StageComp.OpenStageOrChapter(NowChapter, NowStage);
        }
        UI_BattleEnd _ui = Managers.UI.ShowPopupUI<UI_BattleEnd>();
        _ui.SetText(win, _rewardGold, _expStone);
    }


    public void RemoveCreature(GameObject obj, int formationNum)
    {
        
        if (obj.GetComponent<AIController>().CType == Define.CreatureType.CREATURE_HERO)
        {
            heros[formationNum] = null;
            aliveHeros--;
        }

        if (obj.GetComponent<AIController>().CType == Define.CreatureType.CREATURE_ENEMY)
        {
            enemies[formationNum] = null;
            aliveEnemies--;
        }
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

    AIController FindTarget(AIController _pawn)
    {
        AIController target = null;
        List<GameObject> targetList;

        if (_pawn.CType == Define.CreatureType.CREATURE_HERO)
            targetList = enemies;
        else
            targetList = heros;

        for (int i = 0; i <= MAX_CREATURE; i++)
        {
            if (targetList[i] != null)
            {
                target = targetList[i].GetComponent<AIController>();
                break;
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
}
