using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    AIController caster;
    int turn = 0;
    int effectPercentage = 0;


    public void Execute()
    {
        SpawnParticle();
        PlayAnim();
        PlaySound();
        ApplyEffect();
    }
    public Buff Clone(AIController _caster, int _turn, int _effectPercentage)
    {
        Buff newBuff = new Buff();
        SetInfo(_caster, _turn, _effectPercentage);
        return newBuff;
    }
    void SetInfo(AIController _caster, int _turn, int _effectPercentage)
    {
        caster = _caster;
        turn = _turn;
        effectPercentage = _effectPercentage;
    }

    protected virtual void SpawnParticle() { }
    protected virtual void PlayAnim() { }
    protected virtual void PlaySound() { }
    protected virtual void ApplyEffect() { }

}
