using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_1 : Skill
{
    void Awake()
    {
        SType = SkillType.RangeSingle;
    }

    public override void Execute(int heroId)
    {
        // 시전자 방어력 증가 코드 추가
        SpawnSkillPrefab(Caster.gameObject, heroId, 0.5f, 8);
    }
}
