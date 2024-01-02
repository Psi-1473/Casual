using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunDebuff : Buff
{
    public StunDebuff() { isTurnEndBuff = true; }
    protected override void SpawnParticle() { }
    protected override void PlayAnim() { }
    protected override void PlaySound() { }
    protected override bool ApplyEffect()
    {
        // 턴 그냥 넘기기
        return true;
    }

    public override Buff Clone(AIController _caster, int _turn, int _statValue, BuffComponent _owningComp, Define.EBuff _buffType)
    {
        StunDebuff newBuff = new StunDebuff();
        newBuff.SetInfo(_caster, _turn, _statValue, _owningComp, _buffType);
        return newBuff;
    }

    public override void OnEnter(AIController _target, int _statValue) { }
    public override void OnExit(AIController _target) { }
}
