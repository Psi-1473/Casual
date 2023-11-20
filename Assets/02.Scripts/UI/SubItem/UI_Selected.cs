using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Selected : UI_Base
{
    Hero clickedHero = null;
    Hero hero = null;
    int grade = -1;

    enum Images
    {
        Img_Hero,
    }
    
    void Awake()
    {
        Init();
    }

    public override void Init()
    {
        Bind<Image>(typeof(Images));
        Get<Image>((int)Images.Img_Hero).gameObject.SetActive(false);
        BindEvent(gameObject, OnClicked);
    }

    public void SetInfo(Hero _hero, int _grade)
    {
        hero = _hero;
        grade = _grade;
    }

    public void SetClickedHero(Hero _hero)
    {
        Get<Image>((int)Images.Img_Hero).gameObject.SetActive(true);
        clickedHero = _hero;
    }

    void OnClicked(PointerEventData data)
    {
        // 강화재료 UI 띄우기
    }

}
