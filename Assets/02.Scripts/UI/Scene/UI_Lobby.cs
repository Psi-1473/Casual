using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Lobby : UI_Scene
{
    enum Buttons
    {
        Btn_Achievement,
        Btn_Hero,
        Btn_Shop,
        Btn_Spawn,
        Btn_Options,
        Btn_Play
    }

    enum Texts
    {
        Text_Gold,
        Text_Name,
        Text_SpawnGold,
        Text_Level
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));

        BindEvent(GetButton((int)Buttons.Btn_Hero).gameObject, OnHeroClicked);
        BindEvent(GetButton((int)Buttons.Btn_Achievement).gameObject, OnAchieveClicked);
        BindEvent(GetButton((int)Buttons.Btn_Shop).gameObject, OnShopClicked);
        BindEvent(GetButton((int)Buttons.Btn_Spawn).gameObject, OnSpawnClicked);
        BindEvent(GetButton((int)Buttons.Btn_Options).gameObject, OnOptionsClicked);
        BindEvent(GetButton((int)Buttons.Btn_Play).gameObject, OnPlayClicked);
        
        // PointerEventData
        // Action
    }

    public void OnHeroClicked(PointerEventData data)
    {
        Debug.Log("Hero");
    }

    public void OnAchieveClicked(PointerEventData data)
    {
        Debug.Log("Achieve");
    }

    public void OnShopClicked(PointerEventData data)
    {
        Debug.Log("Shop");
    }

    public void OnSpawnClicked(PointerEventData data)
    {
        Debug.Log("Spawn");
    }

    public void OnOptionsClicked(PointerEventData data)
    {
        Debug.Log("Option");
    }

    public void OnPlayClicked(PointerEventData data)
    {
        Debug.Log("Play");
    }
}
