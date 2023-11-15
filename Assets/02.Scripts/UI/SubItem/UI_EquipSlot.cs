using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_EquipSlot : UI_Base
{

    Item equipment;
   // UI_Equipment
    enum Buttons
    {
        Btn_Equip,
    }

    enum Images
    {
        Img_Equipment,
    }

    enum Texts
    {
        Text_Name,
        Text_Power,
        Text_Grade,
    }

    private void Awake()
    {
        Init();
    }

    public override void Init()
    {
        //async
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));

        BindEvent(Get<Button>((int)Buttons.Btn_Equip).gameObject, EquipItem);
    }

    public void SetInfo(Item _equip)
    {
        equipment = _equip;
        Get<Image>((int)Images.Img_Equipment).sprite = Managers.Resource.Load<Sprite>($"Images/Items/{(int)_equip.IType}/{_equip.Id}");

        Get<TextMeshProUGUI>((int)Texts.Text_Name).text = _equip.ItemName;
        Get<TextMeshProUGUI>((int)Texts.Text_Power).text = $"{_equip.Power}";

        string grade = "";
        if (_equip.Grade == 0) grade = "노말";
        else if (_equip.Grade == 1) grade = "레어";
        else grade = "유니크";

        Get<TextMeshProUGUI>((int)Texts.Text_Grade).text = grade;
    }

    void EquipItem(PointerEventData data)
    {
        Debug.Log("Equip !");
    }
}
