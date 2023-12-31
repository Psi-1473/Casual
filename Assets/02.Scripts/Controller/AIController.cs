using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using State = Define.State;
using CreatureType = Define.CreatureType;

public class AIController : MonoBehaviour
{
    Creature originalCreature; // 경험치 적용시킬 용도
    Define.State state = Define.State.Setting;
    Define.CreatureType cType = Define.CreatureType.CREATURE_HERO;
    Animator anim;
    StatComponent stat;
    BuffComponent buffComp;
    float speed = 5.5f;
    
    [SerializeField]
    Slider hpBar;
    Slider mpBar;

    public bool IsDead { get; set; } = false;
    public int FormationNumber { get; set; }
    public Transform FixedTrans { get; set; }
    public Transform CenterTrans { get; set; }
    public GameObject Target { get; set; }
    public StatComponent Stat { get { return stat; } private set { stat = value; } }
    public BuffComponent BuffComp { get { return buffComp; } private set { buffComp = value; } }
    public State CreatureState { get { return state; } set {   state = value; } }
    public CreatureType CType { get { return cType; } private set { cType = value; } }
    public Action OnStatChanged = null;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        stat = GetComponent<StatComponent>();
        buffComp = GetComponent<BuffComponent>();
        transform.GetChild(0).gameObject.AddComponent<CreatureAnimEvent>(); // 
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
            case State.TurnStart:
                OnTurnStart();
                break;
            case State.MoveToEnemy:
                OnMoveToEnemey();
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
        if (Managers.SceneEx.CurrentScene.SceneType != Define.Scene.InGame)
            return;

        Move(FixedTrans.position, 2.5f, State.Idle);
    }
    void OnIdle()
    {
        
    }
    void OnTurnStart()
    {
        if (stat.Mp == 2 && GetComponent<Skill>() != null)
        {
            StopCoroutine("Co_SkillUse");
            StartCoroutine("Co_SkillUse");
        }
        state = State.MoveToEnemy;
    }
    void OnMoveToEnemey()
    {
        Vector3 _dest = new Vector3(Target.transform.position.x + (2.3f * gameObject.transform.localScale.x / 2),
            Target.transform.position.y,
            Target.transform.position.z);

        if (stat.Mp == 2 && GetComponent<Skill>() != null)
        {
            Move(_dest, speed, State.Skill);
            return;
        }
        if (Stat.Role == 2 || Stat.Role == 3)
        {
            state = State.Attack;
            return;
        }

        Move(_dest, speed, State.Attack);
    }
    void OnAttack()
    {
        state = State.Idle;
        Attack(Target.GetComponent<AIController>());
        Target = null;
    }
    void OnSkill()
    {
        state = State.Idle;
        if (Target != null)
        {
            GetComponent<Skill>().Target = Target;
            Target = null;
        }
        stat.Mp = 0;
        anim.SetTrigger("Skill");
        return;
    }
    void OnReturn()
    {
        Move(FixedTrans.position, speed, State.Idle);
    }
    #endregion

    #region Battle Functions (public)
    public void BeginTurn(AIController _target)
    {
        Target = _target.gameObject;
        state = State.Idle;
        buffComp.ExecuteBuffs(FixedTrans);
    }
    public void CompleteBuffExecution(bool _turnEnd)
    {
        if (IsDead)
        {
            Managers.Battle.ProceedPhase();
            return;
        }

        if (!_turnEnd)
        {
            state = State.TurnStart;
            return;
        }

        StopCoroutine("Co_TurnEnd");
        StartCoroutine("Co_TurnEnd", 0.5f);
    }
    public void OnDamaged(int _damage, bool isTrueDamage = false)
    {
        if (!isTrueDamage)
        {
            float armor = stat.Defense * 0.01f;
            int decreasedValue = (int)(_damage * armor);
            _damage -= decreasedValue;
        }
        Stat.Hp = Stat.Hp - _damage;
        
        UI_DamageText _ui = Managers.UI.MakeAnimUI<UI_DamageText>(FixedTrans);
        _ui.SetInfo(_damage);

        if (Stat.Hp <= 0)
            Die();
    }
    public void Heal(float percentage)
    {
        int value = (int)(Stat.MaxHp * percentage);
        Stat.Hp = Stat.Hp + value;
        if (Stat.Hp > Stat.MaxHp)
            Stat.Hp = Stat.MaxHp;
    }
    #endregion

    #region Battle Functions (private)
    void Attack(AIController _target)
    {
        if (GetComponent<Skill>() != null)
            stat.Mp++;

        anim.SetTrigger("Attack");
        _target.OnDamaged(stat.Attack);
    }
    void Move(Vector3 _dest, float _speed, State _nextState)
    {
        if (stat.Mp == 2 && GetComponent<Skill>() != null && _nextState == State.Skill)
        {
            SkillType _sType = GetComponent<Skill>().SType;
            if (_sType == SkillType.RangeSingle || _sType == SkillType.RangeMulti)
                _dest = transform.position;
            if (_sType == SkillType.MeleeMulti)
                _dest = CenterTrans.position;
        }
        else if ((Stat.Role == 2 || Stat.Role == 3) && _nextState == State.Attack)
            _dest = transform.position;

        if (Vector3.Distance(_dest, transform.position) >= 0.03f)
        {
            Vector3 dir = _dest - transform.position;
            transform.position += dir * Time.deltaTime * _speed;
            anim.SetBool("Move", true);
        }
        else if (Vector3.Distance(_dest, transform.position) <= 0.03f)
        {
            anim.SetBool("Move", false);
            if (state == State.Return)
            {
                state = _nextState;
                StopCoroutine("Co_TurnEnd");
                StartCoroutine("Co_TurnEnd", 0.3f);
                return;
            }
            transform.position = _dest;
            state = _nextState;
        }
    }
    void Die()
    {
        if (IsDead)
            return;

        anim.SetTrigger("Death");
        IsDead = true;
        Managers.Battle.RemoveCreature(gameObject, FormationNumber);
        buffComp.Clear();
    }
    #endregion

    #region Setting Functions (public)
    public void SetCreatureStat(Hero _hero, int _id, Transform _trans, int _formation, Transform _center)
    {
        if (_hero == null)
            cType = CreatureType.CREATURE_ENEMY;
        else
            cType = CreatureType.CREATURE_HERO;

        FixedTrans = _trans;
        CenterTrans = _center;
        FormationNumber = _formation;
        stat.SetStat(_hero, _id);
        InitBarUI();
        InitBuffUI();
        SetSkill();
    }
    void SetSkill()
    {
        Type skillType;
        if (cType == CreatureType.CREATURE_HERO)
            skillType = Type.GetType($"Skill_{stat.Id}");
        else
            skillType = Type.GetType($"ESkill_{stat.Id}");

        if (skillType != null)
        {
            gameObject.AddComponent(skillType);
            Skill skill = gameObject.GetComponent(skillType) as Skill;
            skill.Caster = this;
            skill.Center = CenterTrans;

            if(cType == CreatureType.CREATURE_HERO)
                skill.SetBuff(Managers.Data.SkillDict[stat.Id].buffType);
            else
                skill.SetBuff(Managers.Data.EnemySkillDict[stat.Id].buffType);
        }
    }
    void InitBarUI()
    {
        if (Managers.SceneEx.CurrentScene.SceneType != Define.Scene.InGame)
            return;

        UI_StatBar _bar = Managers.UI.MakeWorldSpaceUI<UI_StatBar>(transform);
        _bar.Owner = gameObject;
        _bar.Init();
        _bar.SetPos(GetComponent<SPUM_Prefabs>()._horse);
        _bar.gameObject.transform.localScale *= 2;

    }
    void InitBuffUI()
    {
        if (Managers.SceneEx.CurrentScene.SceneType != Define.Scene.InGame)
            return;

        UI_BuffBar _bar = Managers.UI.MakeWorldSpaceUI<UI_BuffBar>(transform);
        _bar.transform.position = new Vector3 (transform.position.x, transform.position.y + 1.9f, transform.position.z);
        _bar.Owner = gameObject;
        _bar.SetPos(GetComponent<SPUM_Prefabs>()._horse);
        _bar.SetBuffAction(buffComp);
        _bar.gameObject.transform.localScale *= 2;
    }
    #endregion

   
    IEnumerator Co_TurnEnd(float _time)
    {
        yield return new WaitForSeconds(_time);

        Target = null;
        state = State.Idle;
        Managers.Battle.PushOrder(this);
        Managers.Battle.ProceedPhase();
        yield break;
    }
    IEnumerator Co_SkillUse()
    {
        bool isEnemy = (cType == CreatureType.CREATURE_ENEMY) ? true : false;
        UI_SkillUse _ui = Managers.UI.ShowPopupUI<UI_SkillUse>();
        _ui.SetInfo(stat.Id, isEnemy);
        yield break;
    }
}
