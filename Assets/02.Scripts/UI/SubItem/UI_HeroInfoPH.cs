using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_HeroInfoPH : UI_Base
{
    public Hero RegisteredHero { get; private set; }

    UI_PlaceHero placeUI;
    enum Texts
    {
        Text_Name,
        Text_Level,
    }

    enum Images
    {
        Img_Hero,
        Img_Picked,
        Img_Grade,
    }

    void Awake()
    {
        
    }


    public override void Init()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));

        BindEvent(this.gameObject, (data) => { PlaceHero(); });
    }

    public void SetHeroInfo(Hero _hero, UI_PlaceHero _ui)
    {
        RegisteredHero = _hero;
        placeUI = _ui;;

        SetTexts(_hero);
        SetImages(_hero);
    }

    public void PlaceHero()
    {
        int _placeNumber = Managers.GetPlayer.HeroComp.SetHeroFormation(RegisteredHero);

        if (_placeNumber != -1)
        {
            SetHeroInfo(RegisteredHero, placeUI);
            placeUI.PlaceHero(RegisteredHero, _placeNumber);
        }
    }

    void SetTexts(Hero _hero)
    {
        Get<TextMeshProUGUI>((int)Texts.Text_Name).text = _hero.CreatureName;
        Get<TextMeshProUGUI>((int)Texts.Text_Level).text = $"{_hero.Level}";
    }

    void SetImages(Hero _hero)
    {
        int grade = (_hero.Grade == 9) ? 3 : _hero.Grade % 3;
        Sprite _heroSprite = Managers.Resource.Load<Sprite>($"Images/Heros/{_hero.Id}");
        Sprite _gradeSprite = Managers.Resource.Load<Sprite>($"Images/GradeImg/{grade}");

        GetImage((int)Images.Img_Hero).sprite = _heroSprite;
        GetImage((int)Images.Img_Grade).sprite = _gradeSprite;
        GetImage((int)Images.Img_Grade).color = _hero.GetStarColor();

        if (!_hero.IsPicked)
            GetImage((int)Images.Img_Picked).gameObject.SetActive(false);
        else
            GetImage((int)Images.Img_Picked).gameObject.SetActive(true);
    }

}
