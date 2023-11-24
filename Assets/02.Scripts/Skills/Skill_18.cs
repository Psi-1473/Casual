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

            target.GetComponent<AIController>().Heal(Caster.Stat.GetHealPercentage());
            SpawnSkillPrefab(target, heroId, 1f);
            ApplyBuff(target);
        }
    }
}
