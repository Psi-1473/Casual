using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_HeroInfo : UI_Base
{
    UI_Hero heroUI;
    public Hero RegisteredHero{ get; private set; }
    enum Texts
    {
        Text_Name,
        Text_Level,
    }

    enum Images
    {
        Img_HeroImg,
        Img_Class,
        Img_Picked,
        Img_Grade,
        HeroInfoFrame,
    }


    public override void Init()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));

        BindEvent(this.gameObject, (data) => { heroUI.SetHero(RegisteredHero); });
    }

    public void SetHeroInfo(Hero _hero, UI_Hero _ui)
    {
        heroUI = _ui;
        RegisteredHero = _hero;

        SetTexts(_hero);
        SetImages(_hero);
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
        Sprite _frameSprite = Managers.Resource.Load<Sprite>($"Images/ClassFrame/{_hero.Grade / 3}");
        Sprite _roleSprite = Managers.Resource.Load<Sprite>($"Images/ClassImage/{_hero.Role}");
        Sprite _gradeSprite = Managers.Resource.Load<Sprite>($"Images/GradeImg/{grade}");

        GetImage((int)Images.Img_HeroImg).sprite = _heroSprite;
        GetImage((int)Images.HeroInfoFrame).sprite = _frameSprite;
        GetImage((int)Images.Img_Class).sprite = _roleSprite;
        GetImage((int)Images.Img_Grade).sprite = _gradeSprite;
        GetImage((int)Images.Img_Grade).color = _hero.GetStarColor();

        if (!_hero.IsPicked)
            GetImage((int)Images.Img_Picked).gameObject.SetActive(false);
    }

}
