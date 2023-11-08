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
    Return,
}
public enum CreatureType
{
    CREATURE_HERO,
    CREATURE_ENEMY
}


public class AIController : MonoBehaviour
{
    Creature originalCreature;
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
    public GameObject Target { get; set; }
    public StatComponent Stat { get { return stat; } private set { stat = value; } }
    public State CreatureState { get { return state; } set { state = value; } }
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
            case State.Return:
                OnReturn();
                break;
        }
    }

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

        if(stat.Mp == 2)
        {
            stat.Mp = 0;
            // 1. 스킬사용
            // 종료
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
            transform.position += dir * Time.deltaTime * 3.5f;
            anim.SetBool("Move", true);
        }
        else if (Vector3.Distance(_dest, transform.position) <= 0.1f)
        {
            anim.SetBool("Move", false);
            state = State.Idle;
        }
    }
    void OnReturn()
    {
        if (Vector3.Distance(FixedTrans.position, transform.position) >= 0.1f)
        {
            Vector3 dir = FixedTrans.position - transform.position;
            anim.SetBool("Move", true);
            transform.position += dir * Time.deltaTime * 3.5f;

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

    void Attack(AIController _target)
    {
        // 공격하고 자기자리로
        anim.SetTrigger("Attack");
        stat.Mp++;
        StopCoroutine(Co_Wait());
        StartCoroutine(Co_Wait());
        _target.OnDamaged(20);
    }
    void OnDamaged(int _damage)
    {
        // 1. 피격 이펙트 틀기


        // 2. 체력 깎기
        Stat.Hp = Stat.Hp - _damage;
        // 3. 죽었는지 확인
        if (Stat.Hp <= 0)
            Die();
    }
    void Die()
    {
        Debug.Log("사망");

        // 1. 죽는 애니메이션 틀고 방치
        anim.SetTrigger("Death");

        IsDead = true; // 임시

        // 2. Manager 에서 빼줌
        Managers.Battle.RemoveCreature(gameObject, FormationNumber);



    }

    public void SetStateAttack(AIController _target)
    {
        Target = _target.gameObject;
        state = State.Attack;
    }
    public void InitBarUI()
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

   // 스탯 설정하는 함수 생성해야함
    public void SetHeroStat(Hero _hero)
    {
        cType = CreatureType.CREATURE_HERO;
        stat.SetStatByHeroInfo(_hero);
    }

    public void SetEnemyStat(int _enemyId)
    {
        cType = CreatureType.CREATURE_ENEMY;
        stat.SetStatByEnemyInfo(_enemyId);
    }

    IEnumerator Co_Wait()
    {
        yield return new WaitForSeconds(0.5f);
        yield break;

    }
}
