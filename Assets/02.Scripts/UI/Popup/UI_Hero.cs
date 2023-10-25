using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Hero : UI_Popup
{

    enum Buttons
    {
        Btn_Close
    }

    enum Texts
    {
     
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        BindEvent(GetButton((int)Buttons.Btn_Close).gameObject, (data) => { Managers.UI.ClosePopupUI(); } );
    }

}
