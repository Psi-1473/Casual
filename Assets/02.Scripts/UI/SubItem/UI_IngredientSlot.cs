using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_IngredientSlot : UI_Base
{
    Hero hero;
    UI_Ingredients baseUI;
    enum Images
    {
        Img_Hero,
        Img_Grade,
        Img_Selected,
    }
    
    void Awake()
    {
        Init();
    }

    public override void Init()
    {
        Bind<Image>(typeof(Images));
        BindEvent(gameObject, OnClicked);

        SetSelectedImage(false);
    }

    public void SetSelectedImage(bool value)
    {
        GetImage((int)Images.Img_Selected).gameObject.SetActive(value);
    }

    public void SetInfo(Hero _hero, UI_Ingredients _baseUI)
    {
        hero = _hero;
        baseUI = _baseUI;

        Sprite _heroSprite = Managers.Resource.Load<Sprite>($"Images/Heros/{_hero.Id}");
        Sprite _gradeSprite = Managers.Resource.Load<Sprite>($"Images/GradeImg/{_hero.Grade % 3}");

        GetImage((int)Images.Img_Hero).sprite = _heroSprite;
        GetImage((int)Images.Img_Grade).sprite = _gradeSprite;
        GetImage((int)Images.Img_Grade).color = _hero.GetStarColor();
    }

    void OnClicked(PointerEventData data)
    {
        if(baseUI.ClickedUI != null)
            baseUI.ClickedUI.SetSelectedImage(false);

        baseUI.ClickedUI = this;
        baseUI.ClickedHero = hero;
        SetSelectedImage(true);
    }






}
