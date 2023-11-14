using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UI_Hero : UI_Popup
{
    Hero clickedHero = null;

    public Hero ClickedHero { get { return clickedHero; }  set { clickedHero = value; } }
    enum Buttons
    {
        Btn_LevelUp,
    }

    enum Images
    {
        Img_HeroMain,
        Img_HeroClass,
        Img_Skill
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
        Text_SkillName,
        Text_SkillInfo,
        Text_Level,
        Text_Level1,
        Text_Level2,
        Text_Level3,
        Text_ExpStone,
    }

    enum GameObjects
    {
        Content,
        Obj_SkillInfo,
        ClosePopup
    }

    void Awake()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        
        Bind<Image>(typeof(Images));
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));

        Managers.GetPlayer.HeroComp.Sort();
        HeroComponent comp = Managers.GetPlayer.HeroComp;
        //, comp.RareHero, comp.NormalHero
        LoadHeros(comp.Heros);
        SetHero(Get<GameObject>((int)GameObjects.Content).transform.GetChild(0).GetComponent<UI_HeroInfo>().RegisteredHero);

        BindEvent(Get<Image>((int)Images.Img_Skill).gameObject, PopupSkillInfo, Define.UIEvent.Click);
        BindEvent(Get<GameObject>((int)GameObjects.ClosePopup).gameObject, CloseSkillInfo, Define.UIEvent.Click);
        BindEvent(Get<Button>((int)Buttons.Btn_LevelUp).gameObject, ClickLevelUp, Define.UIEvent.Click);

        Get<GameObject>((int)GameObjects.Obj_SkillInfo).gameObject.SetActive(false);
        Get<GameObject>((int)GameObjects.ClosePopup).gameObject.SetActive(false);

    }

    //10개
    void LoadHeros(List<Hero> _heros)
    {
        Managers.GetPlayer.HeroComp.Sort();
        Get<TextMeshProUGUI>((int)Texts.Text_HeroNum).text = $"{_heros.Count}";
       

        for (int i = 0; i < _heros.Count; i++)
        {
            UI_HeroInfo _info = Managers.UI.MakeSubItem<UI_HeroInfo>(Get<GameObject>((int)GameObjects.Content).transform);
            _info.Init();
            _info.SetHeroInfo(_heros[i], this);
        }
    }


    public void SetHero(Hero _hero)
    {

        clickedHero = _hero;
        string role = "";
        string grade = "";
        int maxExp = Managers.Data.ExpDict[clickedHero.Level].exp;
        int nowExp = Managers.GetPlayer.Inven.ExpStone;
        
        Get<TextMeshProUGUI>((int)Texts.Text_HeroName).text = _hero.CreatureName;
        Get<TextMeshProUGUI>((int)Texts.Text_Attack).text = $"{_hero.Attack}";
        Get<TextMeshProUGUI>((int)Texts.Text_Armor).text = $"{_hero.Defense}";
        Get<TextMeshProUGUI>((int)Texts.Text_Level).text = $"Lv. {_hero.Level}";
        Get<TextMeshProUGUI>((int)Texts.Text_ExpStone).text = $"{nowExp} / {maxExp}";



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

        Sprite skill = Managers.Resource.Load<Sprite>($"Images/SkillImage/Hero/{_hero.Id}");

        if (skill != null)
            GetImage((int)Images.Img_Skill).sprite = skill;

        // null 이면 빈 칸으로s
    }

    void PopupSkillInfo(PointerEventData data)
    {
        Debug.Log("Popup Skill Info");
        Get<GameObject>((int)GameObjects.Obj_SkillInfo).gameObject.SetActive(true);
        Get<GameObject>((int)GameObjects.ClosePopup).gameObject.SetActive(true);
        SetSkillInfo();
    }

    void CloseSkillInfo(PointerEventData data)
    {
        Debug.Log("Close Skill Info");
        Get<GameObject>((int)GameObjects.Obj_SkillInfo).gameObject.SetActive(false);
        Get<GameObject>((int)GameObjects.ClosePopup).gameObject.SetActive(false);
        
    }

    void SetSkillInfo()
    {
        if (clickedHero == null)
            return;
        Debug.Log($"스킬 인포 : {Managers.Data.SkillDict[0].name}");

        //SkillInfo _info;
        if (Managers.Data.SkillDict.TryGetValue(clickedHero.Id, out var _info))
        {
            
            Get<TextMeshProUGUI>((int)Texts.Text_SkillName).text = $"{_info.name}";
            Get<TextMeshProUGUI>((int)Texts.Text_SkillInfo).text = $"{_info.description}";
            Get<TextMeshProUGUI>((int)Texts.Text_Level1).text = $"Lv. 1 : {_info.lv1}%";
            Get<TextMeshProUGUI>((int)Texts.Text_Level2).text = $"Lv. 2 : {_info.lv2}%";
            Get<TextMeshProUGUI>((int)Texts.Text_Level3).text = $"Lv. 3 : {_info.lv3}%";
        }
    }

    void ClickLevelUp(PointerEventData data)
    {
        int maxExp = Managers.Data.ExpDict[clickedHero.Level].exp;
        int nowExp = Managers.GetPlayer.Inven.ExpStone;

        if (nowExp < maxExp)
            return;

        Managers.GetPlayer.Inven.ExpStone -= maxExp;
        clickedHero.LevelUp();
        SetHero(ClickedHero);
    }
}
