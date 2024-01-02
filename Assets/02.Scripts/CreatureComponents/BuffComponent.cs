using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BuffComponent : MonoBehaviour
{
    Dictionary<Define.EBuff, Buff> buffDict = new Dictionary<Define.EBuff, Buff>();
    Transform ownerTrans;

    /*
        ������ �߰��ǰų� ������ �� ������ Action - UI ���� ����
     */
    public Action<Define.EBuff> OnBuffAdded;
    public Action<Define.EBuff> OnBuffRemoved;
    
    

    public void GetNewBuff(Define.EBuff _buffType, Buff _buff)
    {
        /*
         * ��ü�� ������ �ο�
         * �̹� ������ ������ ������ �ִٸ� ���� �ֱٿ� ���� ������ �����
         */
        if (buffDict.ContainsKey(_buffType))
            buffDict[_buffType] = _buff;
        else
        {
            buffDict.Add(_buffType, _buff);
            OnBuffAdded.Invoke(_buffType);
        }

    }
    public void RemoveBuff(Define.EBuff _buffType)
    {
        if (!buffDict.ContainsKey(_buffType)) return;

        buffDict[_buffType].OnExit(GetComponent<AIController>());
        buffDict.Remove(_buffType);
        OnBuffRemoved.Invoke(_buffType);
    }

    public void ExecuteBuffs(Transform _trans)
    {
        /*
         * �ܺο��� ȣ���� ������ �ִ� ������ ȿ���� ������ �Լ� ��ü�� ���� ���۵Ǹ� ����ȴ�.
         *  Coroutine�� ���� ����� �� ��ü�� "���� ȿ�� ���� �Ϸ�" �Լ��� �����Ų��.
         */
        ownerTrans = _trans;
        StopCoroutine("Co_ExecuteBuff");
        StartCoroutine("Co_ExecuteBuff");
    }

    public void Clear()
    {
        foreach (var buff in buffDict)
            OnBuffRemoved.Invoke(buff.Key);
    }

    IEnumerator Co_ExecuteBuff()
    {
        bool turnEnd = false;
        List<Define.EBuff> removeBuffs = new List<Define.EBuff>();

        foreach (var buff in buffDict)
        {
            bool playEffectUI = buff.Value.Execute();
            if (buff.Value.IsTurnEndBuff) turnEnd = true;

            if (buff.Value.Turn <= 0)
                removeBuffs.Add(buff.Value.BuffType);

            if (playEffectUI)
            {
                UI_BuffAlarm _ui = Managers.UI.MakeAnimUI<UI_BuffAlarm>(ownerTrans);
                _ui.SetInfo(buff.Value.BuffType);

                yield return new WaitForSeconds(0.5f);
            }
        }

        foreach (var buffType in removeBuffs)
            RemoveBuff(buffType);

        gameObject.GetComponent<AIController>().CompleteBuffExecution(turnEnd);

        yield break;
    }
}
