using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleedDebuff : Buff
{
    public BleedDebuff() { isTurnEndBuff = false; }
    protected override void SpawnParticle() { }
    protected override void PlayAnim() { }
    protected override void PlaySound() { }
    protected override bool ApplyEffect()
    {
        // �ҷ��� ������ �ֱ�
        // + ���� ���� ȿ�� ġ������
        return true;
    }

    public override Buff Clone(AIController _caster, int _turn, int _statValue, BuffComponent _owningComp, Define.EBuff _buffType)
    {
        BleedDebuff newBuff = new BleedDebuff();
        newBuff.SetInfo(_caster, _turn, _statValue, _owningComp, _buffType);
        return newBuff;
    }

    public override void OnEnter(AIController _target, int _statValue) { }
    public override void OnExit(AIController _target) { }
}
