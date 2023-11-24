using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_2 : Skill
{
    void Awake()
    {
        SType = SkillType.Melee;
    }
    public override void Execute(int heroId)
    {
        SpawnSkillPrefab(Target, heroId, 1f, 8);
        ApplyBuff(Target);
    }
}
