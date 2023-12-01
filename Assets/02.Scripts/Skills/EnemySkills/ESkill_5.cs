using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESkill_5 : Skill
{
    void Awake()
    {
        SType = SkillType.RangeSingle;
    }
    public override void Execute(int heroId)
    {
        SpawnSkillPrefab(Target, heroId, 0.5f, 8, true);
        ApplyBuff(Target);
    }


}
