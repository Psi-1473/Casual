using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_BuffAlarm : UI_Anim
{
    enum Texts
    {
        Text_Buff,
    }

    enum Images
    {
        Img_Buff,
    }


    void Awake()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
    }

    public void SetInfo(Define.EBuff _type)
    {
        Get<TextMeshProUGUI>((int)Texts.Text_Buff).text = "버프";
        Debug.Log("버프");
    }

    public void OnExit()
    {
        Destroy(this.gameObject);
    }
}
