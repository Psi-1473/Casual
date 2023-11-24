using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_6 : Skill
{
    void Awake()
    {
        SType = SkillType.MeleeMulti;
    }
    public override void Execute(int heroId)
    {
        List<GameObject> targets = FindFrontEnemies();
        for (int i = 0; i < targets.Count; i++)
        {
            GameObject target = targets[i];
            if (target == null)
                return;

            SpawnSkillPrefab(target, heroId, 0.5f, 8);
            ApplyBuff(target);
        }
    }
}
