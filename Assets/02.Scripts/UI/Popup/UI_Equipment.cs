using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Equipment : UI_Popup
{
    Hero clickedHero;
    UI_Hero baseUI;
    Item item;
    List<UI_EquipSlot> slotList = new List<UI_EquipSlot>();

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

    public void SetSlots(Hero _hero, string _type, UI_Hero _baseUI)
    {
        clickedHero = _hero;
        baseUI = _baseUI;
        SetEquipmentByType(_type);
        CreateNewSlot(_type);
    }
    void SetEquipment(Item _equipment)
    {
        string equipName = "";
        string damage = "";
        string grade = "";
        item = _equipment;

        if (_equipment == null) Get<Image>((int)Images.Img_Equiped).sprite = Managers.Resource.Load<Sprite>("Images/Items/Item_None");

        if (_equipment != null)
        {
            Get<Image>((int)Images.Img_Equiped).sprite = Managers.Resource.Load<Sprite>($"Images/Items/{(int)_equipment.IType}/{_equipment.Id}");
            equipName = _equipment.ItemName;
            damage = (_equipment.ITypeString == "Armor") ? $"���� : {_equipment.Power}" : $"���ݷ� : {_equipment.Power}";

            if (_equipment.Grade == 0) grade = "�븻";
            if (_equipment.Grade == 1) grade = "����";
            if (_equipment.Grade == 2) grade = "����ũ";
        }

        Get<TextMeshProUGUI>((int)Texts.Text_Name).text = equipName;
        Get<TextMeshProUGUI>((int)Texts.Text_Power).text = damage;
        Get<TextMeshProUGUI>((int)Texts.Text_Grade).text = grade;

       
        baseUI.RenewItem();

    }
    public void RenewSlot(string _type)
    {
        SetEquipmentByType(_type);

        List<Item> items = Managers.GetPlayer.Inven.Items[(int)ItemType.Equip];
        List<Item> itemsToSet = new List<Item>();

        int itemCount = 0;
        for (int i = 0; i < items.Count; i++)
            if (items[i].ITypeString == _type)
            {
                itemsToSet.Add(items[i]);
                itemCount += items[i].Number;
            }
        int slotCount = slotList.Count;

        if(itemCount < slotCount)
        {
            UI_EquipSlot _ui = slotList[slotCount - 1];
            slotList.RemoveAt(slotCount - 1);
            Destroy(_ui.gameObject);
        }
        else if(itemCount > slotCount)
        {
            UI_EquipSlot _ui = Managers.UI.MakeSubItem<UI_EquipSlot>(Get<GameObject>((int)GameObjects.Content).transform);
            slotList.Add(_ui);
        }

        int sIdx = 0;
        for (int i = 0; i < itemsToSet.Count; i++)
            for (int j = 0; j < itemsToSet[i].Number; j++)
            {
                slotList[sIdx].SetInfo(clickedHero, itemsToSet[i], this, itemsToSet[i].ITypeString);
                sIdx++;
            } 

    }
    void CreateNewSlot(string _type)
    {
        List<Item> items = Managers.GetPlayer.Inven.Items[(int)ItemType.Equip];

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ITypeString != _type) continue;
            for (int j = 0; j < items[i].Number; j++)
            {
                UI_EquipSlot _ui = Managers.UI.MakeSubItem<UI_EquipSlot>(Get<GameObject>((int)GameObjects.Content).transform);
                _ui.SetInfo(clickedHero, items[i], this, _type);
                slotList.Add(_ui);
            }
        }
    }
    void SetEquipmentByType(string _type)
    {
        if (_type == "Armor") SetEquipment(clickedHero.Armor);
        else SetEquipment(clickedHero.Weapon);
    }

    #region BindFunc
    public void UnEquip(PointerEventData data)
    {
        if (item == null)
            return;

        clickedHero.UnEquipItem(item.ITypeString);
        RenewSlot(item.ITypeString);
    }
    #endregion
}
