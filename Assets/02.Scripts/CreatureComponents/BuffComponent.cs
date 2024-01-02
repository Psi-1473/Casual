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
        버프가 추가되거나 삭제될 때 실행할 Action - UI 갱신 역할
     */
    public Action<Define.EBuff> OnBuffAdded;
    public Action<Define.EBuff> OnBuffRemoved;
    
    

    public void GetNewBuff(Define.EBuff _buffType, Buff _buff)
    {
        /*
         * 객체에 버프를 부여
         * 이미 동일한 버프를 가지고 있다면 가장 최근에 받은 버프로 덮어쓰기
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
         * 외부에서 호출할 가지고 있는 버프의 효과를 실행할 함수 객체의 턴이 시작되면 실행된다.
         *  Coroutine을 통해 실행된 뒤 객체의 "버프 효과 적용 완료" 함수를 실행시킨다.
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
