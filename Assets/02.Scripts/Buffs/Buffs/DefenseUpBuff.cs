using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseUpBuff : Buff
{
    int increasedValue = 0;
    public DefenseUpBuff() { turnEnd = false; }
    protected override void SpawnParticle() { }
    protected override void PlayAnim() { }
    protected override void PlaySound() { }
    protected override void ApplyEffect() { }

    public override Buff Clone(AIController _caster, int _turn, int _effectPercentage, BuffComponent _owningComp, Define.EBuff _buffType)
    {
        DefenseUpBuff newBuff = new DefenseUpBuff();
        newBuff.SetInfo(_caster, _turn, _effectPercentage, _owningComp, _buffType);
        return newBuff;
    }

    public override void OnEnter(AIController _target, int _effectPercentage)
    {
        OnExit(_target);
        int increase = (int)(_target.Stat.Defense * (_effectPercentage * 0.01f));
        _target.Stat.Defense += increase;
        increasedValue = increase;
    }
    public override void OnExit(AIController _target) 
    {
        _target.Stat.Defense -= increasedValue;
        increasedValue = 0;
    }
}
