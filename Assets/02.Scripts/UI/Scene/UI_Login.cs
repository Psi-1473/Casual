using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UI_Login : UI_Scene
{
    enum Buttons
    {
        Btn_Confirm,
    }

    enum GameObjects
    {
        Input_Name,
    }
    void Awake()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));
        BindEvent(Get<Button>((int)Buttons.Btn_Confirm).gameObject, OnConfirmClicked);
    }

    void OnConfirmClicked(PointerEventData data)
    {
        string name = Get<GameObject>((int)GameObjects.Input_Name).GetComponent<TMP_InputField>().text;

        if (name.Length < 2)
            return;

        if (name.Length > 5)
            return;

        Managers.GetPlayer.PlayerName = name;

        Managers.SceneEx.LoadScene(Define.Scene.Lobby);
        
    }
}
