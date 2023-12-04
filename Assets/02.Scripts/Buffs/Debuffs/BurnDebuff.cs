using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnDebuff : Buff
{
    public BurnDebuff() { turnEnd = false; }
    protected override void SpawnParticle() { }
    protected override void PlayAnim() { }
    protected override void PlaySound() { }
    protected override bool ApplyEffect()
    {
        // 소량의 데미지 주기
        int damage = (caster.Stat.Attack / 5);
        if (damage < 1) damage = 1;
        owningComp.gameObject.GetComponent<AIController>().OnDamaged(damage);
        return true;
    }

    public override Buff Clone(AIController _caster, int _turn, int _effectPercentage, BuffComponent _owningComp, Define.EBuff _buffType)
    {
        BurnDebuff newBuff = new BurnDebuff();
        newBuff.SetInfo(_caster, _turn, _effectPercentage, _owningComp, _buffType);
        return newBuff;
    }

    public override void OnEnter(AIController _target, int _effectPercentage) { }
    public override void OnExit(AIController _target) { }
}
