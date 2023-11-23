using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager
{
    public Dictionary<Define.EBuff, Buff> buffDict { get; private set; } = new Dictionary<Define.EBuff, Buff>();

    public void Init()
    {
        FreezeDebuff _freeze = new FreezeDebuff();
        BurnDebuff _burn  = new BurnDebuff();
        BleedDebuff _bleed = new BleedDebuff();

        buffDict.Add(Define.EBuff.Freeze, _freeze);
        buffDict.Add(Define.EBuff.Burn, _burn);
        buffDict.Add(Define.EBuff.Bleed, _bleed);
    }

    void TakeBuff(Define.EBuff _buffType, AIController _caster, AIController _target, int _turn, int _effectPercentage)
    {
        Buff newBuff = buffDict[_buffType].Clone(_caster, _turn, _effectPercentage);
        //_target한테 newBuff 주기
        // 이미 타겟이 그 버프를 가지고 있으면
        // 1. 캐스터 비교, 더 강한 애 버프로 덮어쓰기
        // 2. 남은 턴만 올리기
    }
}
