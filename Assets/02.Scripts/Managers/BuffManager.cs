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
        StunDebuff _stun = new StunDebuff();
        DefenseUpBuff _defenseUp = new DefenseUpBuff();

        buffDict.Add(Define.EBuff.Freeze, _freeze);
        buffDict.Add(Define.EBuff.Burn, _burn);
        buffDict.Add(Define.EBuff.Bleed, _bleed);
        buffDict.Add(Define.EBuff.Stun, _stun);
        buffDict.Add(Define.EBuff.DefenseUp, _defenseUp);
    }

    public void TakeBuff(Define.EBuff _buffType, AIController _caster, AIController _target, int _turn, int _effectPercentage)
    {
        BuffComponent buffComp = _target.GetComponent<BuffComponent>();
        Buff newBuff = buffDict[_buffType].Clone(_caster, _turn, _effectPercentage, buffComp, _buffType);
        newBuff.OnEnter(_target, _effectPercentage);
        buffComp.GetNewBuff(_buffType, newBuff);
    }

    public List<Define.EBuff> GetBuffTypes(string _buffCodeString)
    {
        int _buffCode = ConvertBuffCode(_buffCodeString);

        List<Define.EBuff> eBuffs = new List<Define.EBuff>();

        CheckAndAddBuffType(eBuffs, Define.EBuff.Freeze, _buffCode);
        CheckAndAddBuffType(eBuffs, Define.EBuff.Burn, _buffCode);
        CheckAndAddBuffType(eBuffs, Define.EBuff.Bleed, _buffCode);
        CheckAndAddBuffType(eBuffs, Define.EBuff.Stun, _buffCode);
        CheckAndAddBuffType(eBuffs, Define.EBuff.DefenseUp, _buffCode);


        return eBuffs;
    }

    void CheckAndAddBuffType(List<Define.EBuff> _buffList, Define.EBuff _buffType, int _buffCode)
    {
        if ((_buffCode & (int)_buffType) == (int)_buffType) _buffList.Add(_buffType);
    }

    int ConvertBuffCode(string _code)
    {
        int a = 0 << 8;

        for (int i = 0; i < _code.Length; i++)
        {
            a = a << 1;
            int num = 0 << 7;
            num += _code[i] - '0' << 0;

            a = a | num;
        }
        return a;
    }
}
