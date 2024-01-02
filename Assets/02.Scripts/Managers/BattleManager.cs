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
         * hero, enemy 모두 최대 객체 수가 5, 배열의 1~5 index만 사용 예정이므로 미리 null로 초기화
         * 전투 AI 객체가 전투에 들어가면 자기 자리가 정해져있기 때문에 자리에 맞춰서 객체를 배치하기 위해 미리 1~5번 index를 사용할 수 있도록 초기화
         * ex) hero[1]에 있는 객체는 hero 진영 1번 자리에 배치된 객체
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
         * 전투가 시작되어 전투 씬으로 씬이동이 일어나면 실행
         * 전투 시작 후, 외부에서 heros와 enemies 값을 세팅하면 객체의 공격 순서를 결정하는 리스트를 세팅한 뒤 턴 진행
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
         * 한 턴을 진행하는 함수
         * 공격 순서 큐에서 다음 차례의 객체를 뽑아 공격 타겟을 찾아주고 나머지 객체들의 남은 턴을 갱신한 후, 큐에서 뽑은 객체의 턴을 실행
         * 한 쪽 진영의 남은 객체 수가 0이되면 전투 종료
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
         * 전투의 승/패를 판단하고 보상 지급
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
