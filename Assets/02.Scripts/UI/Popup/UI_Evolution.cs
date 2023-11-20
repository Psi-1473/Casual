using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Evolution : UI_Popup
{
    Hero hero;
    UI_EvolutionSlot clickedSlot;


    enum Buttons
    {
        Btn_Confirm,
    }
    enum Texts
    {
        Text_TargetName,
    }
    enum Images
    {
        Img_TargetHero,
        Img_TargetGrade,
    }
    enum GameObjects
    {
        Condition,
        Content,
        TargetInfo,
    }

    void Awake()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Managers.GetPlayer.HeroComp.Sort();

        CreateSlot();

        Get<GameObject>((int)GameObjects.TargetInfo).SetActive(false);
    }

    public void SetTargetInfo(Hero _hero, UI_EvolutionSlot _slot)
    {
        if (clickedSlot != null)
            clickedSlot.SetSelectedImage(false);

        Get<GameObject>((int)GameObjects.TargetInfo).SetActive(true);
        hero = _hero;
        clickedSlot = _slot;
        if (clickedSlot != null) clickedSlot.SetSelectedImage(true);
        int grade = _hero.Grade / 3;
        Sprite _heroSprite = Managers.Resource.Load<Sprite>($"Images/Heros/{_hero.Id}");

        Get<TextMeshProUGUI>((int)Texts.Text_TargetName).text = _hero.CreatureName;
        GetImage((int)Images.Img_TargetHero).sprite = _heroSprite;

        CreateConditionSlot();
    }

    void CreateSlot()
    {
        List<Hero> heros = Managers.GetPlayer.HeroComp.Heros;

        for(int i = 0; i < heros.Count; i++)
        {
            UI_EvolutionSlot _ui = Managers.UI.MakeSubItem<UI_EvolutionSlot>(Get<GameObject>((int)GameObjects.Content).transform);
            _ui.SetHeroInfo(heros[i], this);   
        }
    }

    void CreateConditionSlot()
    {
        int cnt = Get<GameObject>((int)GameObjects.Condition).transform.childCount;

        for (int i = 0; i < cnt; i++)
            Destroy(Get<GameObject>((int)GameObjects.Condition).transform.GetChild(i).gameObject);

        if (hero == null)
            return;

        if(hero.Grade % 3 == 0 || hero.Grade % 3 == 2)
        {
            UI_Selected _ui = Managers.UI.MakeSubItem<UI_Selected>(Get<GameObject>((int)GameObjects.Condition).transform);
            _ui.SetInfo(hero, SelectedType.SAME_HERO);
        }

        if(hero.Grade % 3 == 1 || hero.Grade % 3 == 2)
        {
            UI_Selected _ui = Managers.UI.MakeSubItem<UI_Selected>(Get<GameObject>((int)GameObjects.Condition).transform);
            _ui.SetInfo(null, SelectedType.SAME_GRADE);
        }

    }

    void ClickConfirm()
    {

    }
}
