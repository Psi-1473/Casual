using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Equipment : UI_Popup
{
    Hero clickedHero;
    int itemCount = 0;
    enum Buttons
    {
        Btn_UnEquip,
    }

    enum Texts
    {
        Text_Name,
        Text_Power,
        Text_Grade,
    }

    enum Images
    {
        Img_Equiped,
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
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));

        BindEvent(Get<Button>((int)Buttons.Btn_UnEquip).gameObject, UnEquip);
    }

    public void UnEquip(PointerEventData data)
    {
        Debug.Log("UnEquip !");
    }

    public void SetSlots(Hero _hero, string _type)
    {
        clickedHero = _hero;
        if(_type == "Armor") SetEquipment(clickedHero.Armor);
        else SetEquipment(clickedHero.Weapon);
        List<Item> items = Managers.GetPlayer.Inven.Items[(int)ItemType.Equip];

        for(int i = 0; i < items.Count; i++)
        {
            if (items[i].ITypeString == _type)
            {
                UI_EquipSlot _ui = Managers.UI.MakeSubItem<UI_EquipSlot>(Get<GameObject>((int)GameObjects.Content).transform);
                _ui.SetInfo(items[i]);
                itemCount++;
            }
        }
    }

    public void SetEquipment(Item _equipment)
    {
        string equipName = "";
        string damage = "";
        string grade = "";

        if(_equipment != null)
        {
            Get<Image>((int)Images.Img_Equiped).sprite = Managers.Resource.Load<Sprite>($"Images/Items/{(int)_equipment.IType}/{_equipment.Id}");
            equipName = _equipment.ItemName;
            damage = (_equipment.ITypeString == "Armor") ? $"방어력 : {_equipment.Power}" : $"공격력 : {_equipment.Power}";

            if (_equipment.Grade == 0) grade = "노말";
            if (_equipment.Grade == 1) grade = "레어";
            if (_equipment.Grade == 2) grade = "유니크";
        }

        Get<TextMeshProUGUI>((int)Texts.Text_Name).text = equipName;
        Get<TextMeshProUGUI>((int)Texts.Text_Power).text = damage;
        Get<TextMeshProUGUI>((int)Texts.Text_Grade).text = grade;

    }

    void RenewSlot(string type)
    {
        
    }
}
