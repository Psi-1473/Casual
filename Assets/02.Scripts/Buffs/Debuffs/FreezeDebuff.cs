using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeDebuff : Buff
{
    protected override void SpawnParticle() { }
    protected override void PlayAnim() { }
    protected override void PlaySound() { }
    protected override void ApplyEffect()
    {
        // 소량의 데미지 주고
        // 턴 그냥 넘기기
        turnEnd = true;
    }

    public override Buff Clone(AIController _caster, int _turn, int _effectPercentage, BuffComponent _owningComp, Define.EBuff _buffType)
    {
        FreezeDebuff newBuff = new FreezeDebuff();
        newBuff.SetInfo(_caster, _turn, _effectPercentage, _owningComp, _buffType);

        Debug.Log("Clone New Buff");
        return newBuff;
    }
}
