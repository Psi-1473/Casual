using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffComponent : MonoBehaviour
{
    Dictionary<Define.EBuff, Buff> buffDict = new Dictionary<Define.EBuff, Buff>();

    public void GetNewBuff(Define.EBuff _buffType, Buff _buff)
    {
        if(buffDict.ContainsKey(_buffType))
            Overwrite(_buffType, _buff);
        else
            buffDict.Add(_buffType, _buff);
    }

    public void RemoveBuff(Define.EBuff _buffType)
    {
        buffDict.Remove(_buffType);
    }
    
    public bool ExecuteBuffs()
    {
        List<Define.EBuff> removeBuffs = new List<Define.EBuff>();
        bool turnEnd = false;
        foreach(var buff in buffDict)
        {
            if (buff.Value.Execute())
            {
                turnEnd = true;
                if (buff.Value.Turn <= 0)
                    removeBuffs.Add(buff.Value.BuffType);
            }
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
