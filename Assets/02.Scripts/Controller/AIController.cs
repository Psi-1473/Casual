using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using State = Define.State;
using CreatureType = Define.CreatureType;


public class AIController : MonoBehaviour
{
    /* Coroutine을 통해 함수 실행 후 일정 시간 대기 시키려할 때 사용할 시간 */
    const float stopDistance = 0.03f;
    const float turnEndDelay = 0.3f;
    const float buffDelay = 0.5f;
    const float skillDelay = 0.5f;

    Creature originalCreature; // 경험치 적용시킬 용도
    State state = State.Setting;
    CreatureType cType;
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
            case State.TurnStart:
                OnTurnStart();
                break;
            case State.MoveToAttack:
                OnMoveToAttack();
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
    /*
        각 상태마다 실행할 Update 함수 모음
     */
    void OnSetting()
    {
        if (Managers.SceneEx.CurrentScene.SceneType != Define.Scene.InGame)
            return;

        Move(FixedTrans.position, 2.5f, State.Idle);
    }
    void OnIdle() { }
    void OnTurnStart()
    {
        if (stat.Mp == 2 && GetComponent<Skill>() != null)
        {
            state = State.Idle;
            StopCoroutine("Co_SkillUse");
            StartCoroutine("Co_SkillUse");
            return;
        }
        state = State.MoveToAttack;
    }
    void OnMoveToAttack()
    {
        Vector3 _dest = new Vector3(Target.transform.position.x + (2.3f * gameObject.transform.localScale.x / 2),
            Target.transform.position.y,
            Target.transform.position.z);

        if (stat.Mp == 2 && GetComponent<Skill>() != null)
        {
            Move(_dest, speed, State.Skill);
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
    /* 전투 관련 함수 : 외부에서도 호출되어야 하는 함수들 */
    public void BeginTurn(AIController _target)
    {
        /*
         * 객체의 턴을 본격적으로 시작하는 함수
         * 타겟을 설정하고 BuffComponent를 통해 자신에게 걸려있는 버프, 디버프의 효과를 먼저 적용
         */
        Target = _target.gameObject;
        state = State.Idle;
        buffComp.ExecuteBuffs(FixedTrans);
    }
    public void CompleteBuffExecution(bool _turnEnd)
    {
        /*
         * BeginTurn을 통해 버프, 디버프의 효과가 모두 발동되면 BuffComponent에 의해 호출
         * _turnEnd : 적용된 버프나 디버프 효과 중 턴을 강제로 종료해야 하는 효과가 있었는지 여부 (ex. 기절 버프가 있었을 경우 행동 불능)
         */
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
        StartCoroutine("Co_TurnEnd", buffDelay);
    }
    public void OnDamaged(int _damage, bool isTrueDamage = false)
    {
        if (!isTrueDamage)
        {
            // 버프 등에 의한 고정 데미지가 아닐 경우에는 방어력 적용
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
    /* 전투 관련 함수 : AI Controller 내부에서만 호출되는 함수들*/
    void Attack(AIController _target)
    {
        if (GetComponent<Skill>() != null)
            stat.Mp++;

        anim.SetTrigger("Attack");
        _target.OnDamaged(stat.Attack);
    }
    void Move(Vector3 _dest, float _speed, State _nextState)
    {
        /* 
         * 객체를 _dest 좌표까지 _speed 만큼의 속도로 이동시킨 뒤 도착하면 해당 객체의 상태를 _nextState로 바꿔준다. 
         * 1. 기본적으로 _dest 변수에는 자기 자신의 자리에 해당하는 좌표나 (공격 후 복귀) 공격하려는 대상에 대한 좌표가 (공격하러 이동) 들어온다.
         * 2. 공격 및 스킬사용을 위한 이동일 경우 공격타입에 따라 (제자리에서 공격해야 하는지 타겟에게 다가가야 하는지 등) 내부적으로 _dest 값을 변경
         */

        if (_nextState == State.Skill)
        {
            SkillType _sType = GetComponent<Skill>().SType;
            if (_sType == SkillType.RangeSingle || _sType == SkillType.RangeMulti)
                _dest = transform.position;
            if (_sType == SkillType.MeleeMulti)
                _dest = CenterTrans.position;
        }
        else if ((Stat.Role == (int)Define.Role.Range || Stat.Role == (int)Define.Role.Sup) && _nextState == State.Attack)
            _dest = transform.position;

        if (Vector3.Distance(_dest, transform.position) >= stopDistance)
        {
            Vector3 dir = _dest - transform.position;
            transform.position += dir * Time.deltaTime * _speed;
            anim.SetBool("Move", true);
        }
        else if (Vector3.Distance(_dest, transform.position) <= stopDistance)
        {
            anim.SetBool("Move", false);
            if (state == State.Return)
            {
                state = _nextState;
                StopCoroutine("Co_TurnEnd");
                StartCoroutine("Co_TurnEnd", turnEndDelay);
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
    // 객체의 세팅과 관련된 함수들
    public void SetCreatureStat(Hero _hero, int _id, Transform _trans, int _formation, Transform _center)
    {
        /* 게임 씬이 전투 씬으로 돌입되어 객체를 생성한 뒤 이 함수를 써서 필요한 정보를 초기에 세팅한다.*/
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
        yield return new WaitForSeconds(skillDelay);
        state = State.MoveToAttack;
        yield break;
    }
}
