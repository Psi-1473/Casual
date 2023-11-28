using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Ingredients : UI_Popup
{
    Hero targetHero;
    Hero clickedHero;
    UI_IngredientSlot clickedUI;
    UI_Selected baseUI;

    public Hero ClickedHero { get { return clickedHero; } set { clickedHero = value; } }
    public UI_IngredientSlot ClickedUI { get { return clickedUI; } set { clickedUI = value; } }
    enum Buttons
    {
        Btn_Confirm,
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
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        BindEvent(Get<Button>((int)Buttons.Btn_Confirm).gameObject, OnClickedBtnConfirm);

    }

    public void CreateSlot(Hero _hero, SelectedType _type, UI_Selected _baseUI)
    {
        targetHero = _hero;
        baseUI = _baseUI;
        if (_type == SelectedType.SAME_HERO) CreateSlotSameHero();
        else if(_type == SelectedType.SAME_GRADE) CreateSlotSameGrade();
    }

    void CreateSlotSameHero()
    {
        List<Hero> heros = Managers.GetPlayer.HeroComp.Heros;

        for(int i = 0; i < heros.Count; i++)
        {
            if (heros[i] == targetHero) continue;
            if (Managers.Upgrade.IsRegisteredHero(heros[i])) continue;
            if (heros[i].IsPicked) continue;

            if (heros[i].Grade == targetHero.Grade && heros[i].Id == targetHero.Id)
            {
                UI_IngredientSlot _ui = Managers.UI.MakeSubItem<UI_IngredientSlot>(Get<GameObject>((int)GameObjects.Content).transform);
                _ui.SetInfo(heros[i], this);
            }
        }
    }

    void CreateSlotSameGrade()
    {
        List<Hero> heros = Managers.GetPlayer.HeroComp.Heros;

        for (int i = 0; i < heros.Count; i++)
        {
            if (heros[i] == targetHero) continue;
            if (Managers.Upgrade.IsRegisteredHero(heros[i])) continue;
            if (heros[i].IsPicked) continue;

            if (heros[i].Grade == targetHero.Grade)
            {
                UI_IngredientSlot _ui = Managers.UI.MakeSubItem<UI_IngredientSlot>(Get<GameObject>((int)GameObjects.Content).transform);
                _ui.SetInfo(heros[i], this);
            }
        }
    }

    void OnClickedBtnConfirm(PointerEventData data)
    {
        if(clickedHero != null)
        {
            baseUI.SetSelectedHero(clickedHero);
            Managers.UI.ClosePopupUI();
        }
    }
}
