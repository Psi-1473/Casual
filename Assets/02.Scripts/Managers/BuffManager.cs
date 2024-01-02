using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager
{
    public Dictionary<Define.EBuff, Buff> BuffDict { get; private set; } = new Dictionary<Define.EBuff, Buff>();

    public void Init()
    {
        FreezeDebuff _freeze = new FreezeDebuff();
        BurnDebuff _burn  = new BurnDebuff();
        BleedDebuff _bleed = new BleedDebuff();
        StunDebuff _stun = new StunDebuff();
        DefenseUpBuff _defenseUp = new DefenseUpBuff();

        BuffDict.Add(Define.EBuff.Freeze, _freeze);
        BuffDict.Add(Define.EBuff.Burn, _burn);
        BuffDict.Add(Define.EBuff.Bleed, _bleed);
        BuffDict.Add(Define.EBuff.Stun, _stun);
        BuffDict.Add(Define.EBuff.DefenseUp, _defenseUp);
    }

    public void TakeBuff(Define.EBuff _buffType, AIController _caster, AIController _target, int _turn, int _statValue)
    {
        BuffComponent buffComp = _target.GetComponent<BuffComponent>();
        Buff newBuff = BuffDict[_buffType].Clone(_caster, _turn, _statValue, buffComp, _buffType);
        newBuff.OnEnter(_target, _statValue);
        buffComp.GetNewBuff(_buffType, newBuff);
    }

    public List<Define.EBuff> GetBuffTypes(string _buffCodeString)
    {
        /*
         * AI 객체에 스킬 클래스가 할당되어 스킬을 세팅할 때 호출된다.
         * 스킬 데이터(json)에 저장되어 있는 buffCode 값을 읽어 해석한 뒤, 해당 스킬이 가지고 있는 버프를 스킬 클래스의 buffTypes 리스트에 추가하는 함수
         */
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
        // _buffCode와 _buffType의 비트연산을 통해 _buffType에 해당하는 버프가 _buffCode에 포함되어 있는지 확인
        if ((_buffCode & (int)_buffType) == (int)_buffType) _buffList.Add(_buffType);
    }

    int ConvertBuffCode(string _code)
    {
        /*
         * string으로 데이터에 저장되어 있는 버프와 관련된 bit flag 정보를 int 값으로 변환
         */
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
