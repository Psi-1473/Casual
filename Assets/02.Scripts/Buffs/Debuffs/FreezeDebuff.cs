using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeDebuff : Buff
{
    public FreezeDebuff() { isTurnEndBuff = true; }
    protected override void SpawnParticle() { }
    protected override void PlayAnim() { }
    protected override void PlaySound() { }
    protected override bool ApplyEffect()
    {
        // 소량의 데미지 주고
        return true;
    }

    public override Buff Clone(AIController _caster, int _turn, int _statValue, BuffComponent _owningComp, Define.EBuff _buffType)
    {
        FreezeDebuff newBuff = new FreezeDebuff();
        newBuff.SetInfo(_caster, _turn, _statValue, _owningComp, _buffType);

        Debug.Log("Clone New Buff");
        return newBuff;
    }

    public override void OnEnter(AIController _target, int _statValue) { }
    public override void OnExit(AIController _target) { }
}
