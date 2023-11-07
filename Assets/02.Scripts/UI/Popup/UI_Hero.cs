using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UI_Hero : UI_Popup
{
    enum Images
    {
        Img_HeroMain,
        Img_HeroClass,
    }

    enum Texts
    {
        Text_MaxNum,
        Text_HeroNum,
        Text_HeroName,
        Text_HeroGrade,
        Text_HeroClass,
        Text_Attack,
        Text_Armor,
    }

    enum GameObjects
    {
        Content,
    }

    void Awake()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));

        Managers.GetPlayer.HeroComp.Sort();
        HeroComponent comp = Managers.GetPlayer.HeroComp;
        LoadHeros(comp.UniqueHero, comp.RareHero, comp.NormalHero);
        SetHero(Get<GameObject>((int)GameObjects.Content).transform.GetChild(0).GetComponent<UI_HeroInfo>().RegisteredHero);
    }

    //10개
    void LoadHeros(List<Hero> unique, List<Hero> rare, List<Hero> normal)
    {
        int sum = 0;
        sum = unique.Count + rare.Count + normal.Count;
        Get<TextMeshProUGUI>((int)Texts.Text_HeroNum).text = $"{sum}";

        for (int i = 0; i < unique.Count; i++)
        {
            UI_HeroInfo _info = Managers.UI.MakeSubItem<UI_HeroInfo>(Get<GameObject>((int)GameObjects.Content).transform);
            _info.Init();
            _info.SetHeroInfo(unique[i], this);
        }

        for (int i = 0; i < rare.Count; i++)
        {
            UI_HeroInfo _info = Managers.UI.MakeSubItem<UI_HeroInfo>(Get<GameObject>((int)GameObjects.Content).transform);
            _info.Init();
            _info.SetHeroInfo(rare[i], this);
        }

        for (int i = 0; i < normal.Count; i++)
        {
            UI_HeroInfo _info = Managers.UI.MakeSubItem<UI_HeroInfo>(Get<GameObject>((int)GameObjects.Content).transform);
            _info.Init();
            _info.SetHeroInfo(normal[i], this);
        }
    }


    public void SetHero(Hero _hero)
    {

        Get<TextMeshProUGUI>((int)Texts.Text_HeroName).text = _hero.CreatureName;
        Get<TextMeshProUGUI>((int)Texts.Text_Attack).text = $"{_hero.Attack}";
        Get<TextMeshProUGUI>((int)Texts.Text_Armor).text = $"{_hero.Defense}";

        string role = "";
        string grade = "";

        switch(_hero.Grade)
        {
            case 0:
                grade = "노말";
                break;
            case 1:
                grade = "레어";
                break;
            case 2:
                grade = "유니크";
                break;
            default:
                break;
        }

        switch (_hero.Role)
        {
            case 0:
                role = "탱커";
                break;
            case 1:
                role = "근거리 딜러";
                break;
            case 2:
                role = "원거리 딜러";
                break;
            case 3:
                role = "서포터";
                break;
            default:
                break;
        }

        Get<TextMeshProUGUI>((int)Texts.Text_HeroGrade).text = grade;
        Get<TextMeshProUGUI>((int)Texts.Text_HeroClass).text = role;

        GetImage((int)Images.Img_HeroMain).sprite = Managers.Resource.Load<Sprite>($"Images/Heros/{_hero.Id}");
        GetImage((int)Images.Img_HeroClass).sprite = Managers.Resource.Load<Sprite>($"Images/ClassImage/{_hero.Role}");


    }
}
