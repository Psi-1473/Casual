using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_StatBar : UI_Base
{
    const float horseHeight = 0.6f;

    public GameObject Owner { get; set; }
    enum GameObjects
    {
        HpBar,
        MpBar,
    }

    private void Update()
    {
        //SetPos();
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        Owner.GetComponent<AIController>().OnStatChanged += SetHpAndMp;
        SetHpAndMp();
    }

    public void SetPos(bool isHorse = false)
    {
        if (Owner == null)
            return;
        float value = 1.2f;
        if (isHorse) value += horseHeight;

        gameObject.transform.position = new Vector3(Owner.transform.position.x, Owner.transform.position.y + value, Owner.transform.position.z);
       
    }

    void SetHpAndMp()
    {
        Get<GameObject>((int)GameObjects.HpBar).GetComponent<Slider>().value = Owner.GetComponent<AIController>().Stat.HpRatio;
        Get<GameObject>((int)GameObjects.MpBar).GetComponent<Slider>().value = Owner.GetComponent<AIController>().Stat.MpRatio;
    }

}
