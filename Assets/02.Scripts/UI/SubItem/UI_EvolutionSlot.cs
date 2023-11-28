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
        Text_MaxGrade,
        Text_Level,
        Text_Formation,
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
        SetTexts(_hero);
        SetImages(_hero);
    }
    public void SetSelectedImage(bool value)
    {
        GetImage((int)Images.Img_Selected).gameObject.SetActive(value);
    }

    void SetTexts(Hero _hero)
    {
        Get<TextMeshProUGUI>((int)Texts.Text_Name).text = _hero.CreatureName;
        Get<TextMeshProUGUI>((int)Texts.Text_Level).text = $"{_hero.Level}";
        if (hero.MaxGrade != hero.Grade)
            Get<TextMeshProUGUI>((int)Texts.Text_MaxGrade).gameObject.SetActive(false);
        else
            Get<TextMeshProUGUI>((int)Texts.Text_MaxGrade).gameObject.SetActive(true);

        if (!_hero.IsPicked)
            Get<TextMeshProUGUI>((int)Texts.Text_Formation).gameObject.SetActive(false);
    }

    void SetImages(Hero _hero)
    {
        int grade = (_hero.Grade == 9) ? 3 : _hero.Grade % 3;
        Sprite _heroSprite = Managers.Resource.Load<Sprite>($"Images/Heros/{_hero.Id}");
        Sprite _gradeSprite = Managers.Resource.Load<Sprite>($"Images/GradeImg/{grade}");

        GetImage((int)Images.Img_Hero).sprite = _heroSprite;
        GetImage((int)Images.Img_Grade).sprite = _gradeSprite;
        GetImage((int)Images.Img_Grade).color = _hero.GetStarColor();
    }


    void OnClicked(PointerEventData data)
    {
        if (hero.MaxGrade == hero.Grade)
            return;

        Managers.Upgrade.Clear();
        baseUI.SetTargetInfo(hero, this);
    }



}
