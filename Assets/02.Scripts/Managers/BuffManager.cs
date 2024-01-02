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
         * AI ��ü�� ��ų Ŭ������ �Ҵ�Ǿ� ��ų�� ������ �� ȣ��ȴ�.
         * ��ų ������(json)�� ����Ǿ� �ִ� buffCode ���� �о� �ؼ��� ��, �ش� ��ų�� ������ �ִ� ������ ��ų Ŭ������ buffTypes ����Ʈ�� �߰��ϴ� �Լ�
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
        // _buffCode�� _buffType�� ��Ʈ������ ���� _buffType�� �ش��ϴ� ������ _buffCode�� ���ԵǾ� �ִ��� Ȯ��
        if ((_buffCode & (int)_buffType) == (int)_buffType) _buffList.Add(_buffType);
    }

    int ConvertBuffCode(string _code)
    {
        /*
         * string���� �����Ϳ� ����Ǿ� �ִ� ������ ���õ� bit flag ������ int ������ ��ȯ
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
