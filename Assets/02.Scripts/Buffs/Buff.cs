using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff
{
    protected Define.EBuff buffType;
    protected BuffComponent owningComp;
    protected AIController caster;
    protected int turn = 0;
    protected int statValue = 0;
    protected bool isTurnEndBuff = false;

    public int Turn { get { return turn; } }
    public bool IsTurnEndBuff { get { return isTurnEndBuff; } }
    public Define.EBuff BuffType { get { return buffType; } }

    public bool Execute()
    {
        SpawnParticle();
        PlayAnim();
        PlaySound();
        turn--;

        return ApplyEffect();
    }

    protected void SetInfo(AIController _caster, int _turn, int _statValue, BuffComponent _owningComp, Define.EBuff _buffType)
    {
        caster = _caster;
        turn = _turn;
        statValue = _statValue;
        owningComp = _owningComp;
        buffType = _buffType;
    }


    public abstract void OnEnter(AIController _target, int _statValue);
    public abstract void OnExit(AIController _target);
    public abstract Buff Clone(AIController _caster, int _turn, int _statValue, BuffComponent _owningComp, Define.EBuff _buffType);


    protected abstract void SpawnParticle();
    protected abstract void PlayAnim();
    protected abstract void PlaySound();
    protected abstract bool ApplyEffect();

}
