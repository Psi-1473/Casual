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
        // ������ ���� ���� �ڵ� �߰�
        SpawnSkillPrefab(Caster.gameObject, heroId, 0.5f, 8);
    }
}
