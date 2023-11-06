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

    public Transform FixedTrans { get; set; }
    public GameObject Target { get; set; }
    public StatComponent Stat { get { return stat; } private set { stat = value; } }
    public State CreatureState { get { return state; } set { state = value; } }

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        stat = GetComponent<StatComponent>();
        InitBarUI();
    }


    protected virtual void FixedUpdate()
    {
        switch(state)
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

    void OnSetting()
    {
        // 타겟이 있으면 실행 안되게
        if (Managers.SceneEx.CurrentScene.SceneType != Define.Scene.InGame)
            return;

        if (Vector3.Distance(FixedTrans.position, transform.position) >= 0.7f)
        {
            Vector3 dir = FixedTrans.position - transform.position;
            transform.position += dir * Time.deltaTime;
            anim.SetBool("Move", true);
        }
        else if (Vector3.Distance(FixedTrans.position, transform.position) <= 0.7f)
        {
            anim.SetBool("Move", false);
            state = State.Idle;
        }
    }

    void OnIdle()
    {
        if (Target == null)
            return;

        Attack(Target.GetComponent<Creature>());
        // 그냥 Idle  딱히 뭐 안함
        // Target이 null이 아니라면 때리고 Return 상태로
    }

    void OnAttack()
    {
        if(Stat.Role == 2 || Stat.Role == 3)
            state = State.Idle;

        Vector3 _dest =  new Vector3(Target.transform.position.x + (2.3f * gameObject.transform.localScale.x / 2),
            Target.transform.position.y,
            Target.transform.position.z);

        if (Vector3.Distance(_dest, transform.position) >= 0.7f)
        {
            Vector3 dir = _dest - transform.position;
            transform.position += dir * Time.deltaTime;
            anim.SetBool("Move", true);
        }
        else if (Vector3.Distance(_dest, transform.position) <= 0.7f)
        {
            anim.SetBool("Move", false);
            state = State.Idle;
        }
        // 근거리라면 타겟에게 이동
        // 아니라면 바로 Idle 상태로 (타겟에 값 넣고)
    }

    void OnReturn()
    {
        if (Vector3.Distance(FixedTrans.position, transform.position) >= 0.7f)
        {
            Vector3 dir = FixedTrans.position - transform.position;
            transform.position += dir * Time.deltaTime;
            anim.SetBool("Move", true);
        }
        else if (Vector3.Distance(FixedTrans.position, transform.position) <= 0.7f)
        {
            anim.SetBool("Move", false);
            Target = null;
            state = State.Idle;
            Managers.Battle.ProceedPhase();
        }

    }

    void Attack(Creature _target)
    {
        // 공격하고 자기자리로
        anim.SetTrigger("Attack");
        _target.OnDamaged();
    }

    void OnDamaged()
    {
        // 1. 애니메이션 실행
        // 2. Hp 깎기,
        // 3. 죽었는지 확인
    }

    void Die()
    {

    }




    void InitBarUI()
    {
        if (Managers.SceneEx.CurrentScene.SceneType != Define.Scene.InGame)
            return;

        UI_StatBar _bar = Managers.UI.MakeWorldSpaceUI<UI_StatBar>(transform);
        _bar.Owner = gameObject;

        if (GetComponent<SPUM_Prefabs>()._horse == true)
            _bar.SetHorse();
    }


    

    
}
