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
        Text_Gold,
        Text_Exp,
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

    public void SetText(bool _win, int _gold, int _exp)
    {
        if (!_win)
            Get<GameObject>((int)GameObjects.Text_Ment).GetComponent<TextMeshProUGUI>().text = "ฦะ น่";

        Get<GameObject>((int)GameObjects.Text_Gold).GetComponent<TextMeshProUGUI>().text = $"{_gold}";
        Get<GameObject>((int)GameObjects.Text_Exp).GetComponent<TextMeshProUGUI>().text = $"{_exp}";


    }
    
}
