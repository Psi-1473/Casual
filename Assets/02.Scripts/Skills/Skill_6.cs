using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_6 : Skill
{
    void Awake()
    {
        SType = SkillType.Melee;
    }
    public override void Execute(int heroId)
    {
        SpawnSkillPrefab(Target, heroId, 0f, 5f);
    }
}
