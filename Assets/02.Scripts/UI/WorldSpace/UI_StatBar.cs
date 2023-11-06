using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StatBar : UI_Base
{
    public GameObject Owner { get; set; }
    float horseHeight = 0;
    enum GameObjects
    {
        HpBar,
        MpBar,
    }

    private void Update()
    {
        SetPos();
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        Owner.GetComponent<Creature>().OnStatChanged += SetHpAndMp;
        if(Owner.transform.localScale.x < 0)
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * -1f, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        SetHpAndMp();
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
        Get<GameObject>((int)GameObjects.HpBar).GetComponent<Slider>().value = 0.5f;//Owner.GetComponent<Creature>().Stat.HpRatio;
        Get<GameObject>((int)GameObjects.MpBar).GetComponent<Slider>().value = 0.5f;//Owner.GetComponent<Creature>().Stat.MpRatio;

        Debug.Log($"{Owner.GetComponent<Creature>().Stat.HpRatio}");
    }

}
