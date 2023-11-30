using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InGame : UI_Scene
{
    enum GameObjects
    {
        H_Transform_1,
        H_Transform_2,
        H_Transform_3,
        H_Transform_4,
        H_Transform_5,
        E_Transform_1,
        E_Transform_2,
        E_Transform_3,
        E_Transform_4,
        E_Transform_5,
        Transform_Center,
    }

    void Awake()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));


        Managers.UI.ShowPopupUI<UI_InGameMask>();
    }

}
