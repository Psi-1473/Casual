using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum State
{
    Setting,
    Idle,
    Attack,
    Skill,
    Return,
}
public enum CreatureType
{
    CREATURE_HERO,
    CREATURE_ENEMY
}


public class AIController : MonoBehaviour
{
    Creature originalCreature; // 경험치 적용시킬 용도
    State state = State.Setting;
    CreatureType cType = CreatureType.CREATURE_HERO;
    Animator anim;
    StatComponent stat;

    
    [SerializeField]
    Slider hpBar;
    Slider mpBar;

    public bool IsDead { get; set; } = false;
    public int FormationNumber { get; set; }
    public Transform FixedTrans { get; set; }
    public Transform CenterTrans { get; set; }
    public GameObject Target { get; set; }
    public StatComponent Stat { get { return stat; } private set { stat = value; } }
    public State CreatureState { get { return state; } set { 
            if(value == State.Return)
            {             
                StopCoroutine(Co_Wait());
                StartCoroutine(Co_Wait());
            }
            state = value;
        } }
    public CreatureType CType { get { return cType; } private set { cType = value; } }
    public Action OnStatChanged = null;


    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        stat = GetComponent<StatComponent>();
        transform.GetChild(0).gameObject.AddComponent<CreatureAnimEvent>();
 
    }

    void Update()
    {
        switch (state)
        {
            case State.Setting:
                OnSetting();
                break;
            case State.Idle:
                OnIdle();
                break;
            case State.Attack:
                OnAttack();
                break;
            case State.Skill:
                OnSkill();
                break;
            case State.Return:
                OnReturn();
                break;
        }
    }

    #region Update Functions
    void OnSetting()
    {
        // 타겟이 있으면 실행 안되게
        if (Managers.SceneEx.CurrentScene.SceneType != Define.Scene.InGame)
            return;

        if (Vector3.Distance(FixedTrans.position, transform.position) >= 0.1f)
        {
            Vector3 dir = FixedTrans.position - transform.position;
            transform.position += dir * Time.deltaTime * 1.5f;
            anim.SetBool("Move", true);
        }
        else if (Vector3.Distance(FixedTrans.position, transform.position) <= 0.1f)
        {
            anim.SetBool("Move", false);
            state = State.Idle;
        }
    }
    void OnIdle()
    {
        if (Target == null)
            return;
        else
        {
            Attack(Target.GetComponent<AIController>());
            Target = null;
        }
    }
    void OnAttack()
    {

        if(stat.Mp == 2 && GetComponent<Skill>() != null)
        {
            state = State.Skill;
            return;
        }
        if (Stat.Role == 2 || Stat.Role == 3)
        {
            state = State.Idle;
            return;
        }

        Vector3 _dest = new Vector3(Target.transform.position.x + (2.3f * gameObject.transform.localScale.x / 2),
            Target.transform.position.y,
            Target.transform.position.z);

        if (Vector3.Distance(_dest, transform.position) >= 0.1f)
        {
            Vector3 dir = _dest - transform.position;
            transform.position += dir * Time.deltaTime * 4.5f;
            anim.SetBool("Move", true);
        }
        else if (Vector3.Distance(_dest, transform.position) <= 0.1f)
        {
            anim.SetBool("Move", false);
            state = State.Idle;
        }
    }
    void OnSkill()
    {
        if (Target != null)
        {
            GetComponent<Skill>().Target = Target;
            Target = null;
        }
        stat.Mp = 0;
        if(GetComponent<Skill>().SType == SkillType.RangeSingle || GetComponent<Skill>().SType == SkillType.RangeMulti)
        {
            state = State.Idle;
            anim.SetTrigger("Skill");
            return;
        }

  

        GameObject sTarget = GetComponent<Skill>().Target;
        Vector3 _dest;

        if (GetComponent<Skill>().SType == SkillType.MeleeMulti)
            _dest = CenterTrans.position;
        else
            _dest = new Vector3(sTarget.transform.position.x + (2.3f * gameObject.transform.localScale.x / 2),
               sTarget.transform.position.y,
               sTarget.transform.position.z);

        if (Vector3.Distance(_dest, transform.position) >= 0.1f)
        {
            Vector3 dir = _dest - transform.position;
            transform.position += dir * Time.deltaTime * 4.5f;
            anim.SetBool("Move", true);
        }
        else if (Vector3.Distance(_dest, transform.position) <= 0.1f)
        {
            anim.SetBool("Move", false);
            state = State.Idle;
            anim.SetTrigger("Skill");
        }
    }
    void OnReturn()
    {
        if (Vector3.Distance(FixedTrans.position, transform.position) >= 0.1f)
        {
            Vector3 dir = FixedTrans.position - transform.position;
            anim.SetBool("Move", true);
            transform.position += dir * Time.deltaTime * 4.5f;

        }
        else if (Vector3.Distance(FixedTrans.position, transform.position) <= 0.1f)
        {
            anim.SetBool("Move", false);
            Target = null;
            state = State.Idle;
            Managers.Battle.PushOrder(this);
            Managers.Battle.ProceedPhase();
        }

    }
    #endregion

    #region Battle Functions
    void Attack(AIController _target)
    {
        // 공격하고 자기자리로
        anim.SetTrigger("Attack");
        stat.Mp++;
        StopCoroutine(Co_Wait());
        StartCoroutine(Co_Wait());
        _target.OnDamaged(stat.Attack);
    }
    public void OnDamaged(int _damage)
    {
        Debug.Log($"Damaged! : {_damage}");
        Stat.Hp = Stat.Hp - _damage;
        if (Stat.Hp <= 0)
            Die();
    }
    public void Heal(float percentage)
    {
        int value = (int)(Stat.MaxHp * percentage);
        Stat.Hp = Stat.Hp + value;
        if (Stat.Hp > Stat.MaxHp)
            Stat.Hp = Stat.MaxHp;
        Debug.Log($"Heal! : + {value},  {Stat.Hp}");
        

        
    }
    void Die()
    {
        if (IsDead)
            return;

        anim.SetTrigger("Death");
        IsDead = true;
        Managers.Battle.RemoveCreature(gameObject, FormationNumber);
    }
    #endregion

    #region Setting Functions (public)
    public void SetHeroStat(Hero _hero, Transform _trans, int _formation, Transform _center)
    {
        cType = CreatureType.CREATURE_HERO;
        FixedTrans = _trans;
        CenterTrans = _center;
        FormationNumber = _formation;
        stat.SetStatByHeroInfo(_hero);
        InitBarUI();
        SetSkill();
    }
    public void SetEnemyStat(int _enemyId, Transform _trans, int _formation, Transform _center)
    {
        cType = CreatureType.CREATURE_ENEMY;
        FixedTrans = _trans;
        CenterTrans = _center;
        FormationNumber = _formation;
        stat.SetStatByEnemyInfo(_enemyId);
        InitBarUI();
        
        //SetSkill();
    }
    public void SetStateAttack(AIController _target)
    {
        Debug.Log($"Set Attack : {gameObject.name}, Target : {_target}");
        Target = _target.gameObject;
        state = State.Attack;
    }
    #endregion

    #region Setting Functions (private)
    void SetSkill()
    {
        Type skillType;
        if (cType == CreatureType.CREATURE_HERO)
            skillType = Type.GetType($"Skill_{stat.Id}");
        else
            skillType = Type.GetType($"EnemySkill_{stat.Id}");

        if (skillType != null)
        {
            gameObject.AddComponent(skillType);
            Skill skill = gameObject.GetComponent(skillType) as Skill;
            skill.Caster = this;
            skill.Center = CenterTrans;
        }

        
    }
    void InitBarUI()
    {
        if (Managers.SceneEx.CurrentScene.SceneType != Define.Scene.InGame)
            return;

        UI_StatBar _bar = Managers.UI.MakeWorldSpaceUI<UI_StatBar>(transform);
        _bar.Owner = gameObject;
        _bar.Init();
        _bar.gameObject.transform.localScale *= 2;

        if (GetComponent<SPUM_Prefabs>()._horse == true)
            _bar.SetHorse();
    }
    #endregion


    IEnumerator Co_Wait()
    {
        yield return new WaitForSeconds(0.5f);
        yield break;

    }
}
