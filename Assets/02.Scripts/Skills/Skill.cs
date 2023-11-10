using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    Melee,
    MeleeMulti,
    RangeSingle,
    RangeMulti,
}
public abstract class Skill : MonoBehaviour
{
    public SkillType SType { get; set; }
    public AIController Caster { get; set; }
    public GameObject Target { get; set; }
    public Transform Center { get; set; }

    // 1. ¿¸¡∂ ¿Ã∆Â∆Æ
    abstract public void Execute(int heroId);
    // 2. Hit ¿Ã∆Â∆Æ

    protected void SpawnSkillPrefab(GameObject _target, int _heroId, float yPos = 0, float _skillSize = 2f)
    {
        GameObject obj = Managers.Resource.Instantiate($"SkillEffect/Skill{_heroId}");
        obj.GetComponent<SkillAnimEvent>().Owner = Caster;
        obj.GetComponent<SkillAnimEvent>().Target = _target;
        obj.transform.localScale *= _skillSize;
        obj.transform.position = new Vector3(_target.transform.position.x, _target.transform.position.y + yPos, _target.transform.position.z);
    }
    protected void SpawnSkillPrefab(Transform _target, int _heroId, float yPos = 0, float _skillSize = 2f)
    {
        GameObject obj = Managers.Resource.Instantiate($"SkillEffect/Skill{_heroId}");
        obj.GetComponent<SkillAnimEvent>().Owner = Caster;
        obj.transform.localScale *= _skillSize;
        obj.transform.position = new Vector3(_target.position.x, _target.position.y + yPos, _target.position.z);
    }
    protected List<GameObject> FindAllEnemies()
    {
        List<GameObject> targets = new List<GameObject>();

        for(int i = 0; i < Managers.Battle.Enemies.Count; i++)
        {
            if (Managers.Battle.Enemies[i] != null)
                targets.Add(Managers.Battle.Enemies[i]);
        }

        return targets;
    }
    protected List<GameObject> FindFrontEnemies()
    {
        List<GameObject> targets = new List<GameObject>();

        for (int i = 1; i < 3; i++)
        {
            if (Managers.Battle.Enemies[i] != null)
                targets.Add(Managers.Battle.Enemies[i]);
        }

        if(targets.Count == 0)
        {
            for (int i = 3; i < 6; i++)
            {
                if (Managers.Battle.Enemies[i] != null)
                    targets.Add(Managers.Battle.Enemies[i]);
            }
        }
        return targets;
    }
    protected List<GameObject> FindBackEnemies()
    {
        List<GameObject> targets = new List<GameObject>();

        for (int i = 3; i < 6; i++)
        {
            if (Managers.Battle.Enemies[i] != null)
                targets.Add(Managers.Battle.Enemies[i]);
        }

        if (targets.Count == 0)
        {
            for (int i = 1; i < 3; i++)
            {
                if (Managers.Battle.Enemies[i] != null)
                    targets.Add(Managers.Battle.Enemies[i]);
            }
        }
        return targets;
    }
    protected List<GameObject> FindAllHeros()
    {
        List<GameObject> heros = new List<GameObject>();

        for (int i = 0; i < Managers.Battle.Heros.Count; i++)
        {
            if (Managers.Battle.Heros[i] != null)
                heros.Add(Managers.Battle.Heros[i]);
        }

        return heros;
    }
}
