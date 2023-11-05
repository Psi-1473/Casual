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
        // �� �̰� �����Ϸ��ϱ� �� ����������.. �׳� ���·� ��������!!!!!!!!
        // Enter �̷��ű��� �����ϰ� ���� ���� Update�� OnMove(), OnIdle(), OnAttack ������ ����
        // Attack�� ��, Ÿ���� �ְ� �ٰŸ��� Ÿ�� ������ �̵�, �ƴ϶�� 

        // 1. ���Ÿ� ĳ�������� �ٰŸ� ĳ�������� �Ǻ� (�����Ϳ� ���Ÿ� �������� �Ǻ��� �� �ִ� ������ �߰��ϱ�)

        // 2. (�ٰŸ� ĳ����) Ÿ�ٿ��� ���� (�ڷ�ƾ���� �����ϰ� �ؼ� �ٰŸ� ĳ���͸� 0.3�� ����ϵ��� �ϰ� �ƴ϶�� �ٷ� �����ϵ���)

        // 3. ���� ��� ���ϰ� Ÿ�ٿ��� ������ ���� (������ ���� ���� �ǰ� �ִϸ��̼� ����)

        // 4. ���� ���� �ֳ� �Ǻ��ϴ� �Լ� (BattleManager���� ��������)
        // 4-1) ���� ���� ���ٸ� BattleManager�� ���� ���� �Լ� �����ϰ� ���� ����

        // 5. (�ٰŸ� ĳ����) ���ڸ��� ���ư�

        // 6. �� ������ Battle Manager�� order�� �ٽ� ����ֱ�

        // 7. BattleManager�� ProceedPhase �Լ� �����Ͽ� ���� ��� ����
    }

    void Move()
    {
        // Ÿ���� ������ ���� �ȵǰ�
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
        // 1. Ÿ�� �������� �̵�
        // 2. if(Ÿ�� �ٷ� �ձ��� ������) Attack -> Target Null
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
        // ��Ʋ ���� �� ���ڸ��� ���� ����
    }

    void OnIdle()
    {
        // �׳� Idle  ���� �� ����
        // Target�� null�� �ƴ϶�� ������ Return ���·�
    }

    void OnAttack()
    {
        // �ٰŸ���� Ÿ�ٿ��� �̵�
        // �ƴ϶�� �ٷ� Idle ���·� (Ÿ�ٿ� �� �ְ�)
    }

    void OnReturn()
    {
        // 1. �ڱ��ڸ��� �̵�
        // 2. ���� ��ġ�� �ڱ� �ڸ���  Target = null, ��Ʋ�Ŵ����� Proceed Phase ����
        // 3. ���� Idle��

    }

    
}
