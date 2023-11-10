using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_0 : Skill
{
    void Awake()
    {
        SType = SkillType.RangeMulti;
    }

    public override void Execute(int heroId)
    {
        
        List<GameObject> targets = FindAllHeros();
        for (int i = 0; i < targets.Count; i++)
        {
            GameObject target = targets[i];
            if (target == null)
                return;
            // 타겟 방어력 증가 코드 추가
            SpawnSkillPrefab(target, heroId, 0.5f, 8);
        }
    
    }
}
