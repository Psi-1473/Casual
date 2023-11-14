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

        Get<TextMeshProUGUI>((int)Texts.Text_Name).text = _hero.CreatureName;
        Get<TextMeshProUGUI>((int)Texts.Text_Level).text = $"{_hero.Level}";

        Sprite _heroSprite = Managers.Resource.Load<Sprite>($"Images/Heros/{_hero.Id}");
        Sprite _frameSprite = Managers.Resource.Load<Sprite>($"Images/ClassFrame/{_hero.Grade}");
        Sprite _roleSprite = Managers.Resource.Load<Sprite>($"Images/ClassImage/{_hero.Role}");

        GetImage((int)Images.Img_HeroImg).sprite = _heroSprite;
        GetImage((int)Images.HeroInfoFrame).sprite = _frameSprite;
        GetImage((int)Images.Img_Class).sprite = _roleSprite;

        if (!_hero.IsPicked)
            GetImage((int)Images.Img_Picked).gameObject.SetActive(false);
    }

}
