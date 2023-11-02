using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Close : UI_Base
{
    enum Buttons
    {
        UI_Close
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    { 
        Bind<Button>(typeof(Buttons));
        BindEvent(GetButton((int)Buttons.UI_Close).gameObject, (data) => { OnExit(); Managers.UI.ClosePopupUI();  });
    }

    public void OnExit()
    {
        UI_Popup _ui = GetComponentInParent<UI_Popup>();
        _ui.OnExit();
    }
}
