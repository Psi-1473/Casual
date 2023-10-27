using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_HeroInfo : UI_Base
{
    UI_Hero heroUI;
    public int HeroId { get; private set; }
    enum Texts
    {
        Text_Name,
    }

    enum Images
    {
        Img_HeroImg,
        Img_Class,
        HeroInfoFrame,
    }


    public override void Init()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));

        BindEvent(this.gameObject, (data) => { heroUI.SetHero(HeroId); });
    }

    public void SetHeroInfo(int _heroId, UI_Hero _ui)
    {
        heroUI = _ui;
        HeroId = _heroId;
        HeroInfo hero = Managers.Data.HeroDict[_heroId];
        Get<TextMeshProUGUI>((int)Texts.Text_Name).text = hero.name;

        Sprite _heroSprite = Managers.Resource.Load<Sprite>($"Images/Heros/{_heroId}");
        Sprite _frameSprite = Managers.Resource.Load<Sprite>($"Images/ClassFrame/{hero.grade}");
        Sprite _roleSprite = Managers.Resource.Load<Sprite>($"Images/ClassImage/{hero.role}");

        GetImage((int)Images.Img_HeroImg).sprite = _heroSprite;
        GetImage((int)Images.HeroInfoFrame).sprite = _frameSprite;
        GetImage((int)Images.Img_Class).sprite = _roleSprite;
    }

}
