using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Creature : MonoBehaviour
{
    protected Animator anim;
    protected StatComponent stat;
    [SerializeField]
    protected Slider hpBar;
    protected Slider mpBar;
    protected int Id;

    public Transform Destination { get; set; }
    public bool CanMove { get; set; } = false;
    public StatComponent Stat { get { return stat; } private set { stat = value; } }

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        stat = GetComponent<StatComponent>();
        InitBarUI();
    }


    protected virtual void FixedUpdate()
    {
        Move();
    }

    public void MoveTo(Transform _trans)
    {
        Destination = _trans;
        CanMove = true;
    }

    public void Attack(Creature _target)
    {
        // 아 이거 구현하려니까 넘 복잡해진다.. 그냥 상태로 구현하자!!!!!!!!
        // Enter 이런거까지 자잘하게 넣진 말고 Update에 OnMove(), OnIdle(), OnAttack 정도만 구현
        // Attack일 땐, 타겟이 있고 근거리면 타겟 앞으로 이동, 아니라면 

        // 1. 원거리 캐릭터인지 근거리 캐릭터인지 판별 (데이터에 원거리 유닛인지 판별할 수 있는 데이터 추가하기)

        // 2. (근거리 캐릭터) 타겟에게 접근 (코루틴으로 실행하게 해서 근거리 캐릭터면 0.3초 대기하도록 하고 아니라면 바로 공격하도록)

        // 3. 어택 모션 취하고 타겟에게 데미지 입힘 (데미지 입은 적은 피격 애니메이션 실행)

        // 4. 남은 적이 있나 판별하는 함수 (BattleManager에서 생성예정)
        // 4-1) 남은 적이 없다면 BattleManager의 전투 종료 함수 실행하고 전투 종료

        // 5. (근거리 캐릭터) 제자리로 돌아감

        // 6. 이 유닛을 Battle Manager의 order에 다시 집어넣기

        // 7. BattleManager의 ProceedPhase 함수 실행하여 전투 계속 진행
    }

    void Move()
    {
        // 타겟이 있으면 실행 안되게
        if (CanMove == false) return;

        if (Vector3.Distance(Destination.position, transform.position) >= 0.7f)
        {
            Vector3 dir = Destination.position - transform.position;
            transform.position += dir * Time.deltaTime;
            anim.SetBool("Move", true);
        }
        else if (Vector3.Distance(Destination.position, transform.position) <= 0.7f)
        {
            CanMove = false;
            anim.SetBool("Move", false);
        }
    }

    void MoveToTaget()
    {
        // 1. 타겟 방향으로 이동
        // 2. if(타겟 바로 앞까지 갔으면) Attack -> Target Null
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

    void OnSetting()
    {
        // 배틀 시작 전 제자리로 가는 상태
    }

    void OnIdle()
    {
        // 그냥 Idle  딱히 뭐 안함
        // Target이 null이 아니라면 때리고 Return 상태로
    }

    void OnAttack()
    {
        // 근거리라면 타겟에게 이동
        // 아니라면 바로 Idle 상태로 (타겟에 값 넣고)
    }

    void OnReturn()
    {
        // 1. 자기자리로 이동
        // 2. 현재 위치가 자기 자리면  Target = null, 배틀매니저의 Proceed Phase 실행
        // 3. 상태 Idle로

    }

    
}
