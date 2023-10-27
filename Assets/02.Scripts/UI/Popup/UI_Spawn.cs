using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Spawn : UI_Popup
{
    enum Buttons
    {
        Btn_NormalSpawn,
        Btn_RareSpawn,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));

        BindEvent(GetButton((int)Buttons.Btn_NormalSpawn).gameObject, OnClickedNormalSpawn);
        BindEvent(GetButton((int)Buttons.Btn_RareSpawn).gameObject, OnClickedRareSpawn);

    }

    void OnClickedNormalSpawn(PointerEventData data)
    {
        Managers.Sunmmon.NormalSummon();
    }

    void OnClickedRareSpawn(PointerEventData data)
    {
        Managers.Sunmmon.RareSummon();
    }

}
