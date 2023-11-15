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
                _ui.SetInfo(clickedHero, items[i], this, _type);
                slotList.Add(_ui);
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

    public void RenewSlot(string _type, bool _isSwap)
    {
        if (_type == "Armor") SetEquipment(clickedHero.Armor);
        else SetEquipment(clickedHero.Weapon);

        // 1. 장착 해제로 슬롯이 하나 더 생겨야 하는 경우
        // 2. 스와핑이라 슬롯 신경 안써도 될 경우
        // 3. 장착인데 원래 끼던 무기가 없어서 슬롯이 하나 사라져야 하는 경우


        // 방법 1) 슬롯을 다 지우고 인벤토리 돌면서 다시 생성
        // 방법 2) 슬롯 UI를 리스트로 가지게 한 뒤
        //          1. List[Index] 체크
        //          2. null이 아니라면 정보만 수정
        //          3. null이라면 ui 생성 후 정보 업데이트
        //          4. 아이템 수보다 슬롯 수가 많으면 그 이후는 삭제

        // 일단 방법2 ㄱㄱ
        // 리스트는 만들었고 위에 적어놓은 1~4 알고리즘만

        
    }
}
