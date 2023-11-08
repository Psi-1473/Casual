using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_13 : Skill
{
    void Awake()
    {
        SType = SkillType.RangeMulti;
    }
    public override void Execute(int heroId)
    {
        List<GameObject> targets = FindAllEnemies();
        for(int i = 0; i < targets.Count; i++)
        {
            GameObject target = targets[i];
            if (target == null)
                return;

            SpawnSkillPrefab(target, heroId, 1f);
        }
    }
}
