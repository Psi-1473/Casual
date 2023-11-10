using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_18 : Skill
{
    private void Awake()
    {
        SType = SkillType.RangeMulti;
    }
    public override void Execute(int heroId)
    {
        List<GameObject> heros = FindAllHeros();
        for (int i = 0; i < heros.Count; i++)
        {
            GameObject target = heros[i];
            if (target == null)
                return;

            // 힐 주는 효과 추가하기
            SpawnSkillPrefab(target, heroId, 1f);
        }
    }
}
