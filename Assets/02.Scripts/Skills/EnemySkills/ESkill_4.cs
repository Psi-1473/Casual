using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESkill_4 : Skill
{
    void Awake()
    {
        SType = SkillType.Melee;
    }
    public override void Execute(int heroId)
    {
        SpawnSkillPrefab(Target, heroId, 0.5f, 8, true);
        ApplyBuff(Target);
    }
}
