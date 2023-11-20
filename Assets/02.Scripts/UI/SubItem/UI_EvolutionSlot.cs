using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_EvolutionSlot : UI_Base
{
    UI_Evolution baseUI;
    Hero hero;

    enum Images
    {
        Img_Hero,
        Img_Grade,
        Img_Selected,
    }
    enum Texts
    {
        Text_Name,
    }
    void Awake()
    {
        Init();
    }

    public override void Init()
    {
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));

        SetSelectedImage(false);
        BindEvent(gameObject, OnClicked);
    }

    public void SetHeroInfo(Hero _hero, UI_Evolution _base)
    {
        hero = _hero;
        baseUI = _base;
        Sprite _heroSprite = Managers.Resource.Load<Sprite>($"Images/Heros/{_hero.Id}");
        Sprite _gradeSprite = Managers.Resource.Load<Sprite>($"Images/GradeImg/{_hero.Grade % 3}");

        Get<TextMeshProUGUI>((int)Texts.Text_Name).text = _hero.CreatureName;
        GetImage((int)Images.Img_Hero).sprite = _heroSprite;
        GetImage((int)Images.Img_Grade).sprite = _gradeSprite;
        GetImage((int)Images.Img_Grade).color = _hero.GetStarColor();
    }
    public void SetSelectedImage(bool value)
    {
        GetImage((int)Images.Img_Selected).gameObject.SetActive(value);
    }
    void OnClicked(PointerEventData data)
    {
        baseUI.SetTargetInfo(hero, this);
    }



}
