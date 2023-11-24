using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff
{
    protected Define.EBuff buffType;
    protected BuffComponent owningComp;
    protected AIController caster;
    protected int turn = 0;
    protected int effectPercentage = 0;
    protected bool turnEnd = false;

    public int Turn { get { return turn; } }
    public Define.EBuff BuffType { get { return buffType; } }

    public bool Execute()
    {
        SpawnParticle();
        PlayAnim();
        PlaySound();
        ApplyEffect();

        turn--;
        Debug.Log($"Left Turn {turn}");
        //if (turn <= 0)
        //{
        //    //owningComp.RemoveBuff(buffType);
        //    Debug.Log("Ready to Remove");
        //}
        return turnEnd;
    }

    protected void SetInfo(AIController _caster, int _turn, int _effectPercentage, BuffComponent _owningComp, Define.EBuff _buffType)
    {
        caster = _caster;
        turn = _turn;
        effectPercentage = _effectPercentage;
        owningComp = _owningComp;
        buffType = _buffType;
    }

    public abstract Buff Clone(AIController _caster, int _turn, int _effectPercentage, BuffComponent _owningComp, Define.EBuff _buffType);


    protected abstract void SpawnParticle();
    protected abstract void PlayAnim();
    protected abstract void PlaySound();
    protected abstract void ApplyEffect();

}