using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_AtkDfsUp : UI_Anim
{
    enum Texts
    {
        Text_Value,
    }

    private void Start()
    {
        PlayAnim();

    }

    private void Awake()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<TextMeshProUGUI>(typeof(Texts));
    }
    public void SetValue(int value)
    {
        Get<TextMeshProUGUI>((int)Texts.Text_Value).text = $"{value}";
    }
    void PlayAnim()
    {
        GetComponent<Animator>().SetTrigger("Start");
    }
}
