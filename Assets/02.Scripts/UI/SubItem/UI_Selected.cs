using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum SelectedType
{
    SAME_HERO,
    SAME_GRADE,
}

public class UI_Selected : UI_Base
{
    SelectedType type;
    Hero baseHero = null;
    Hero selectedHero = null;

    public Hero SelectedHero { get { return selectedHero;  } set { selectedHero = value; } }

    enum Images
    {
        Img_Hero,
    }
    
    void Awake()
    {
        Init();
    }

    public override void Init()
    {
        Bind<Image>(typeof(Images));
        Get<Image>((int)Images.Img_Hero).gameObject.SetActive(false);
        BindEvent(gameObject, OnClicked);
    }

    public void SetInfo(Hero _hero, SelectedType _type)
    {
        baseHero = _hero;
        type = _type;
    }

    public void SetSelectedHero(Hero _hero)
    {
        Get<Image>((int)Images.Img_Hero).gameObject.SetActive(true);
        selectedHero = _hero;
        Sprite _heroSprite = Managers.Resource.Load<Sprite>($"Images/Heros/{_hero.Id}");
        GetImage((int)Images.Img_Hero).sprite = _heroSprite;
        // 이미지 불러오기
    }

    void OnClicked(PointerEventData data)
    {
        // 정보를 추가하자
        // SameHero Slot 개수
        // SameGrade Slot 개수
        UI_Ingredients _ui = Managers.UI.ShowPopupUI<UI_Ingredients>();
        _ui.CreateSlot(baseHero, type, this);
    }

}
