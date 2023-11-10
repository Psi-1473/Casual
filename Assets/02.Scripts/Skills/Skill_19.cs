using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_19 : Skill
{
    private void Awake()
    {
        SType = SkillType.RangeSingle;
    }
    public override void Execute(int heroId)
    {
        GameObject targetHero = null;
        List<GameObject> heros = FindAllHeros();
        for (int i = 0; i < heros.Count; i++)
        {
            GameObject target = heros[i];
            if (target == null)
                return;

            if (targetHero == null)
            {
                targetHero = target;
                continue;
            }

            if (targetHero.GetComponent<AIController>().Stat.Hp > target.GetComponent<AIController>().Stat.Hp)
                targetHero = target;
        }


        // 힐 주는 효과 추가하기
        SpawnSkillPrefab(targetHero, heroId, 1f);
    }
}
