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
    }
}
