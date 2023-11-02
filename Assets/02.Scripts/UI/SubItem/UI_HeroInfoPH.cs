using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_HeroInfoPH : UI_Base
{
    public int HeroId { get; private set; }

    UI_PlaceHero placeUI;
    enum Texts
    {
        Text_Grade,
        Text_Name,
    }

    enum Images
    {
        Img_Hero,
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

    public void SetHeroInfo(int _heroId, UI_PlaceHero _ui)
    {
        HeroId = _heroId;
        placeUI = _ui;

        HeroInfo hero = Managers.Data.HeroDict[_heroId];
        Get<TextMeshProUGUI>((int)Texts.Text_Name).text = hero.name;

        string _grade = "";
        switch(hero.grade)
        {
            case 0:
                _grade = "노말";
                break;
            case 1:
                _grade = "레어";
                break;
            case 2:
                _grade = "유니크";
                break;
            default:
                break;

        }
        Get<TextMeshProUGUI>((int)Texts.Text_Grade).text = _grade;
        Sprite _heroSprite = Managers.Resource.Load<Sprite>($"Images/Heros/{_heroId}");
        GetImage((int)Images.Img_Hero).sprite = _heroSprite;
    }

    public void PlaceHero()
    {
        int _placeNumber = Managers.GetPlayer.HeroComp.SetHeroFormation(HeroId);

        if (_placeNumber != -1)
            placeUI.PlaceHero(HeroId, _placeNumber);
    }

}
