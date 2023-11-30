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
        string buffName = "";

        switch(_type)
        {
            case Define.EBuff.Freeze:
                buffName = "ºù °á";
                break;
            case Define.EBuff.Burn:
                buffName = "È­ »ó";
                break;
            case Define.EBuff.Bleed:
                buffName = "Ãâ Ç÷";
                break;
            case Define.EBuff.Stun:
                buffName = "±â Àý";
                break;
        }
        Get<TextMeshProUGUI>((int)Texts.Text_Buff).text = buffName;
        string imgName = System.Enum.GetName(typeof(Define.EBuff), _type);
        Get<Image>((int)Images.Img_Buff).sprite = Managers.Resource.Load<Sprite>($"Images/BuffImages/{imgName}");
    }

    public void OnExit()
    {
        Destroy(this.gameObject);
    }
}
