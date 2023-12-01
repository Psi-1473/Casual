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
    List<Define.EBuff> buffTypes;

    public SkillType SType { get; set; }
    public AIController Caster { get; set; }
    public GameObject Target { get; set; }
    public Transform Center { get; set; }


    // 1. 전조 이펙트
    abstract public void Execute(int heroId);
    // 2. Hit 이펙트
    
    public void SetBuff(string _buffCode)
    {
        buffTypes = Managers.BuffMgr.GetBuffTypes(_buffCode);

        Debug.Log($"버프 세팅 완료 : 버프 개수 = {buffTypes.Count}");
    }


    protected void SpawnSkillPrefab(GameObject _target, int _heroId, float yPos = 0, float _skillSize = 2f, bool isEnemy = false)
    {
        GameObject obj;
        if (!isEnemy)
            obj = Managers.Resource.Instantiate($"SkillEffect/Skill{_heroId}");
        else
            obj = Managers.Resource.Instantiate($"SkillEffect/EnemySkills/ESkill{_heroId}");

        obj.GetComponent<SkillAnimEvent>().Owner = Caster;
        obj.GetComponent<SkillAnimEvent>().Target = _target;
        obj.transform.localScale *= _skillSize;
        obj.transform.position = new Vector3(_target.transform.position.x, _target.transform.position.y + yPos, _target.transform.position.z);
    }
    protected void SpawnSkillPrefab(Transform _target, int _heroId, float yPos = 0, float _skillSize = 2f, bool isEnemy = false)
    {
        GameObject obj;
        if (isEnemy)
            obj = Managers.Resource.Instantiate($"SkillEffect/Skill{_heroId}");
        else
            obj = Managers.Resource.Instantiate($"SkillEffect/EnemySkills/ESkill{_heroId}");
        obj.GetComponent<SkillAnimEvent>().Owner = Caster;
        obj.transform.localScale *= _skillSize;
        obj.transform.position = new Vector3(_target.position.x, _target.position.y + yPos, _target.position.z);
    }


    protected void ApplyBuff(GameObject _target, int _turn = 2, int _effectPercentage = 0)
    {
        for (int i = 0; i < buffTypes.Count; i++)
        {
            Managers.BuffMgr.TakeBuff(buffTypes[i], Caster, _target.GetComponent<AIController>(), _turn, _effectPercentage);
            //Debug.Log($"Apply Buff {buffTypes[i]}");
        }
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
