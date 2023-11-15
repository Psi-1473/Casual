using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Equipment : UI_Popup
{
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

    public void SetSlots(string type)
    {
        List<Item> items = Managers.GetPlayer.Inven.Items[(int)ItemType.Equip];
        for(int i = 0; i < items.Count; i++)
        {
            if (items[i].ITypeString == type)
            {
                UI_EquipSlot _ui = Managers.UI.MakeSubItem<UI_EquipSlot>(Get<GameObject>((int)GameObjects.Content).transform);
                _ui.SetInfo(items[i]);
                itemCount++;
            }
        }
    }

    void RenewSlot(string type)
    {
        
    }
}
