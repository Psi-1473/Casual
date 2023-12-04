using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BuffComponent : MonoBehaviour
{
    Dictionary<Define.EBuff, Buff> buffDict = new Dictionary<Define.EBuff, Buff>();
    Transform ownerTrans;

    public Action<Define.EBuff> OnBuffAdded; // InGame 중, Buff UI에 해당 버프 띄우기
    public Action<Define.EBuff> OnBuffRemoved; // InGame 중, Buff UI에서 해당 버프 지우기
    

    public void GetNewBuff(Define.EBuff _buffType, Buff _buff)
    {
        if (buffDict.ContainsKey(_buffType))
            Overwrite(_buffType, _buff);
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
        ownerTrans = _trans;
        StopCoroutine("Co_ExecuteBuff");
        StartCoroutine("Co_ExecuteBuff");
    }

    public Buff GetBuff(Define.EBuff _buffType)
    {
        if (buffDict.ContainsKey(_buffType))
            return buffDict[_buffType];

        return null;
    }

    public void Clear()
    {
        foreach (var buff in buffDict)
            OnBuffRemoved.Invoke(buff.Key);
    }

    void Overwrite(Define.EBuff _buffType, Buff _buff)
    {
        buffDict[_buffType] = _buff;
    }

    IEnumerator Co_ExecuteBuff()
    {
        bool turnEnd = false;

        foreach (var buff in buffDict)
            if (buff.Value.TurnEnd)
                turnEnd = true;

        List<Define.EBuff> removeBuffs = new List<Define.EBuff>();
        foreach (var buff in buffDict)
        {
            buff.Value.Execute();

            if (buff.Value.Turn <= 0)
                removeBuffs.Add(buff.Value.BuffType);

            UI_BuffAlarm _ui = Managers.UI.MakeAnimUI<UI_BuffAlarm>(ownerTrans);
            _ui.SetInfo(buff.Value.BuffType);
            
            yield return new WaitForSeconds(0.5f);

        }

        foreach (var buffType in removeBuffs)
            RemoveBuff(buffType);

        gameObject.GetComponent<AIController>().BuffToAttack(turnEnd);

        yield break;
    }
}
