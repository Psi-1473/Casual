using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_StatBar : UI_Base
{
    public GameObject Owner { get; set; }
    float horseHeight = 0;
    enum GameObjects
    {
        HpBar,
        MpBar,
    }

    private void Awake()
    {
        Init();
    }
    private void Update()
    {
        SetPos();
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
    }

    public void SetHorse()
    {
        horseHeight = 0.6f;
    }

    void SetPos()
    {
        if (Owner == null)
            return;

        gameObject.transform.position = new Vector3(Owner.transform.position.x, Owner.transform.position.y + 1.2f + horseHeight, Owner.transform.position.z);
    }

    void SetHpAndMp()
    {

    }

}
