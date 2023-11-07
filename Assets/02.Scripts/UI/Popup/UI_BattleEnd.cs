using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_BattleEnd : UI_Popup
{
    enum GameObjects
    {
        Text_Ment,
        Btn_End,
    }

    void Awake()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        Canvas canvas = GetComponent<Canvas>();
        canvas.sortingOrder = 10;
        BindEvent(Get<GameObject>((int)GameObjects.Btn_End), (data) => { Managers.SceneEx.LoadScene(Define.Scene.Lobby); });
    }

    public void SetText(bool _win)
    {
        if (!_win)
            Get<GameObject>((int)GameObjects.Text_Ment).GetComponent<TextMeshProUGUI>().text = "ฦะ น่";
    }
    
}
