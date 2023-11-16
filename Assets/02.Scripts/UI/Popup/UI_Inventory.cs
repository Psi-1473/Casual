using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Inventory : UI_Popup
{
    enum Buttons
    {
        Btn_Equip,
        Btn_Miscellaneous,
    }
    enum GameObjects
    {
        GridBox,
    }
    
    void Awake()
    {
        Init();
        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));

        BindEvent(Get<Button>((int)Buttons.Btn_Equip).gameObject, SetEquips);
        BindEvent(Get<Button>((int)Buttons.Btn_Miscellaneous).gameObject, SetMisc);

        //List<Item> items = Managers.
    }

    public override void Init()
    {
        base.Init();
    }

    public void SetItems(ItemType _type)
    {
        if (_type == ItemType.Equip)
            SetEquips();
        else if (_type == ItemType.Misc)
            SetMisc();
    }

    void SetEquips(PointerEventData data = null)
    {
        Clear();
        List<Item> items = Managers.GetPlayer.Inven.Items[(int)ItemType.Equip];

        for (int i = 0; i < items.Count; i++)
        {
            UI_InvenSlot _ui = Managers.UI.MakeSubItem<UI_InvenSlot>(Get<GameObject>((int)GameObjects.GridBox).transform);
            _ui.SetItem(items[i]);
        }
    }
    void SetMisc(PointerEventData data = null)
    {
        Clear();
        List<Item> items = Managers.GetPlayer.Inven.Items[(int)ItemType.Misc];

        for (int i = 0; i < items.Count; i++)
        {
            UI_InvenSlot _ui = Managers.UI.MakeSubItem<UI_InvenSlot>(Get<GameObject>((int)GameObjects.GridBox).transform);
            _ui.SetItem(items[i]);
        }
    }
    void Clear()
    {
        for(int i = 0; i < Get<GameObject>((int)GameObjects.GridBox).transform.childCount; i++)
        {
            Destroy(Get<GameObject>((int)GameObjects.GridBox).transform.GetChild(i).gameObject);
        }
    }
}
