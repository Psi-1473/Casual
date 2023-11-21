using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 1. 선택된 캐릭터 정보 업뎃
// 2. 가진 영웅 정보 업뎃
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

        BindEvent(Get<Button>((int)Buttons.Btn_Confirm).gameObject, ClickConfirm);

        CreateSlot();

        Get<GameObject>((int)GameObjects.TargetInfo).SetActive(false);

    }

    public void SetTargetInfo(Hero _hero, UI_EvolutionSlot _slot)
    {
        if (clickedSlot != null)
            clickedSlot.SetSelectedImage(false);

        if (_hero == null)
        {
            Get<GameObject>((int)GameObjects.TargetInfo).SetActive(false);
            return;
        }

        Managers.Upgrade.MainHero = _hero;
        hero = _hero;
        clickedSlot = _slot;

        Get<GameObject>((int)GameObjects.TargetInfo).SetActive(true);

        if (clickedSlot != null) 
            clickedSlot.SetSelectedImage(true);

        SetTargetInfoImages(_hero);
        CreateIngredientSlot();
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
    void RenewSlot()
    {
        Managers.GetPlayer.HeroComp.Sort();
        List<Hero> heros = Managers.GetPlayer.HeroComp.Heros; //4
        int leftSlotCount = Get<GameObject>((int)GameObjects.Content).transform.childCount; //5

        int slotCount = leftSlotCount - heros.Count; // 1

        for (int i = leftSlotCount - 1; i >= leftSlotCount - slotCount; i--)
            Destroy(Get<GameObject>((int)GameObjects.Content).transform.GetChild(i).gameObject);

        for (int i = 0; i < heros.Count; i++)
        {
            UI_EvolutionSlot _ui = Get<GameObject>((int)GameObjects.Content).transform.GetChild(i).GetComponent<UI_EvolutionSlot>();
            _ui.SetHeroInfo(heros[i], this);
        }
    }
    void CreateIngredientSlot()
    {
        int cnt = Get<GameObject>((int)GameObjects.Condition).transform.childCount;

        for (int i = 0; i < cnt; i++)
            Destroy(Get<GameObject>((int)GameObjects.Condition).transform.GetChild(i).gameObject);

        if (hero == null)
            return;

        UpgradeInfo info = Managers.Data.UpgradeDict[hero.Grade];

        for(int i = 0; i < info.sameHero; i++)
        {
            UI_Selected _ui = Managers.UI.MakeSubItem<UI_Selected>(Get<GameObject>((int)GameObjects.Condition).transform);
            _ui.SetInfo(hero, SelectedType.SAME_HERO);
        }

        for (int i = 0; i < info.sameGrade; i++)
        {
            UI_Selected _ui = Managers.UI.MakeSubItem<UI_Selected>(Get<GameObject>((int)GameObjects.Condition).transform);
            _ui.SetInfo(hero, SelectedType.SAME_GRADE);
        }
    }

    void SetTargetInfoImages(Hero _hero)
    {
        int grade = (_hero.Grade == 9) ? 3 : _hero.Grade % 3;
        Sprite _heroSprite = Managers.Resource.Load<Sprite>($"Images/Heros/{_hero.Id}");
        Sprite _gradeSprite = Managers.Resource.Load<Sprite>($"Images/GradeImg/{grade}");

        Get<TextMeshProUGUI>((int)Texts.Text_TargetName).text = _hero.CreatureName;
        GetImage((int)Images.Img_TargetHero).sprite = _heroSprite;
        GetImage((int)Images.Img_TargetGrade).sprite = _gradeSprite;
        GetImage((int)Images.Img_TargetGrade).color = _hero.GetStarColor();
    }

    public override void OnExit()
    {
        base.OnExit();

        Managers.Upgrade.Clear();
    }
    #region BindFunc
    void ClickConfirm(PointerEventData data)
    {
        if (Managers.Upgrade.Upgrade())
        {
            Managers.Upgrade.Clear();
            RenewSlot();
            for (int i = 0; i < Get<GameObject>((int)GameObjects.Condition).transform.childCount; i++)
                Destroy(Get<GameObject>((int)GameObjects.Condition).transform.GetChild(i).gameObject);

            SetTargetInfo(null, clickedSlot);

        }
    }
    #endregion
}
