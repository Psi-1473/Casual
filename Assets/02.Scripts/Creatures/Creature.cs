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
public class Creature : MonoBehaviour
{
    
    protected State state = State.Setting;
    protected Animator anim;
    protected StatComponent stat;
    [SerializeField]
    protected Slider hpBar;
    protected Slider mpBar;
    protected int Id;

    public bool IsDead { get; set; } = false;
    public int FormationNumber { get; set; }
    public Transform FixedTrans { get; set; }
    public GameObject Target { get; set; }
    public StatComponent Stat { get { return stat; } private set { stat = value; } }
    public State CreatureState { get { return state; } set { state = value; } }

    public Action OnStatChanged = null;
    

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        stat = GetComponent<StatComponent>();
        transform.GetChild(0).gameObject.AddComponent<CreatureAnimEvent>();
        //InitBarUI();
    }

    protected virtual void Update()
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

    public void SetStateAttack(Creature _target)
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

    void OnSetting()
    {
        // Ÿ���� ������ ���� �ȵǰ�
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
            Attack(Target.GetComponent<Creature>());
            Target = null;
        }

        
        // �׳� Idle  ���� �� ����
        // Target�� null�� �ƴ϶�� ������ Return ���·�
    }

    void OnAttack()
    {
        if (Stat.Role == 2 || Stat.Role == 3)
        {
            state = State.Idle;
            return;
        }

        Vector3 _dest =  new Vector3(Target.transform.position.x + (2.3f * gameObject.transform.localScale.x / 2),
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
        // �ٰŸ���� Ÿ�ٿ��� �̵�
        // �ƴ϶�� �ٷ� Idle ���·� (Ÿ�ٿ� �� �ְ�)
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

    void Attack(Creature _target)
    {
        // �����ϰ� �ڱ��ڸ���
        anim.SetTrigger("Attack");
        StopCoroutine(Co_Wait());
        StartCoroutine(Co_Wait());
        _target.OnDamaged(20);
    }

    void OnDamaged(int _damage)
    {
        // 1. �ǰ� ����Ʈ Ʋ��


        // 2. ü�� ���
        Stat.Hp = Stat.Hp - _damage;
        // 3. �׾����� Ȯ��
        if(Stat.Hp <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("���");

        // 1. �״� �ִϸ��̼� Ʋ�� ��ġ
        anim.SetTrigger("Death");

        IsDead = true; // �ӽ�

        // 2. Manager ���� ����
        Managers.Battle.RemoveCreature(gameObject, FormationNumber);


        
    }




   


    IEnumerator Co_Wait()
    {
        yield return new WaitForSeconds(0.5f);
        yield break;

    }

    
}
