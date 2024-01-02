using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseUpBuff : Buff
{
    int increasedValue = 0;
    public DefenseUpBuff() { isTurnEndBuff = false; }
    protected override void SpawnParticle() { }
    protected override void PlayAnim() { }
    protected override void PlaySound() { }
    protected override bool ApplyEffect() { return false; }

    public override Buff Clone(AIController _caster, int _turn, int _statValue, BuffComponent _owningComp, Define.EBuff _buffType)
    {
        DefenseUpBuff newBuff = new DefenseUpBuff();
        newBuff.SetInfo(_caster, _turn, _statValue, _owningComp, _buffType);
        return newBuff;
    }

    public override void OnEnter(AIController _target, int _statValue)
    {
        OnExit(_target);
        int increasedValue = statValue;
        _target.Stat.Defense += increasedValue;
    }
    public override void OnExit(AIController _target) 
    {
        _target.Stat.Defense -= increasedValue;
        increasedValue = 0;
    }
}
