using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_DamageText : UI_Anim
{
    enum Texts
    {
        Text_Damage,
    }

    void Awake()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<TextMeshProUGUI>(typeof(Texts));
    }

    public void SetInfo(int _damage)
    {
        Get<TextMeshProUGUI>((int)Texts.Text_Damage).text = $"- {_damage}";
    }

    public void OnExit()
    {
        Destroy(gameObject);
    }
}
