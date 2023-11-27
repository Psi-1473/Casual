using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffComponent : MonoBehaviour
{
    Dictionary<Define.EBuff, Buff> buffDict = new Dictionary<Define.EBuff, Buff>();

    public Action<Define.EBuff> OnBuffAdded;
    public Action<Define.EBuff> OnBuffRemoved;


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
        buffDict.Remove(_buffType);
        OnBuffRemoved.Invoke(_buffType);
    }
    
    public bool ExecuteBuffs()
    {
        // 단계를 나눌까 나누자
        List<Define.EBuff> removeBuffs = new List<Define.EBuff>();
        bool turnEnd = false;
        foreach(var buff in buffDict)
        {
            if (buff.Value.Execute())
                turnEnd = true;
                
            if (buff.Value.Turn <= 0)
                removeBuffs.Add(buff.Value.BuffType);

        }

        foreach (var buffType in removeBuffs)
            RemoveBuff(buffType);

        return turnEnd;
    }

    public Buff GetBuff(Define.EBuff _buffType)
    {
        if (buffDict.ContainsKey(_buffType))
            return buffDict[_buffType];

        return null;
    }

    void Overwrite(Define.EBuff _buffType, Buff _buff)
    {
        buffDict[_buffType] = _buff;
    }

    
}
