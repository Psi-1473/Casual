using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleedDebuff : Buff
{
    protected override void SpawnParticle() { }
    protected override void PlayAnim() { }
    protected override void PlaySound() { }
    protected override void ApplyEffect()
    {
        // 소량의 데미지 주기
        // + 고유 지속 효과 치유감소
    }

    public override Buff Clone(AIController _caster, int _turn, int _effectPercentage, BuffComponent _owningComp, Define.EBuff _buffType)
    {
        BleedDebuff newBuff = new BleedDebuff();
        newBuff.SetInfo(_caster, _turn, _effectPercentage, _owningComp, _buffType);
        return newBuff;
    }
}
