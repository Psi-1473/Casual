using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    Melee,
    RangeSingle,
    RangeMulti,
}
public abstract class Skill : MonoBehaviour
{
    public SkillType SType { get; set; }
    public AIController Caster { get; set; }
    public GameObject Target { get; set; }

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
    
}
