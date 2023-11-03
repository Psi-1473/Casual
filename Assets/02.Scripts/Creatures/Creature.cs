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

    void Move()
    {
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
