using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunDebuff : Buff
{
    protected override void SpawnParticle() { }
    protected override void PlayAnim() { }
    protected override void PlaySound() { }
    protected override void ApplyEffect()
    {
        // �� �׳� �ѱ��
        
    }

    public override Buff Clone(AIController _caster, int _turn, int _effectPercentage, BuffComponent _owningComp, Define.EBuff _buffType)
    {
        StunDebuff newBuff = new StunDebuff();
        newBuff.SetInfo(_caster, _turn, _effectPercentage, _owningComp, _buffType);
        return newBuff;
    }
}
