using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop : UI_Popup
{
    List<ItemInfo> meleeWeapons = new List<ItemInfo>();
    List<ItemInfo> rangeWeapons = new List<ItemInfo>();
    List<ItemInfo> armors = new List<ItemInfo>();
    List<ItemInfo> misc = new List<ItemInfo>();
    List<UI_ShopSlot> slots = new List<UI_ShopSlot>();
    enum Buttons
    {
        Btn_MeleeWeapon,
        Btn_RangeWeapon,
        Btn_Armor,
        Btn_Misc,
    }

    enum GameObjects
    {
        Content,
    }

    void Awake()
    {
        Init();
    }

    void Update()
    {
        
    }
    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));
        SetItems();
        CreateSlots(meleeWeapons);

        BindEvent(Get<Button>((int)Buttons.Btn_MeleeWeapon).gameObject, (data) => { CreateSlots(meleeWeapons); });
        BindEvent(Get<Button>((int)Buttons.Btn_RangeWeapon).gameObject, (data) => { CreateSlots(rangeWeapons); });
        BindEvent(Get<Button>((int)Buttons.Btn_Armor).gameObject, (data) => { CreateSlots(armors); });
        BindEvent(Get<Button>((int)Buttons.Btn_Misc).gameObject, (data) => { CreateSlots(misc); });
    }

    void SetItems()
    {
        foreach(var item in Managers.Data.EquipDict)
        {
            if (item.Value.itemType == "Sword") meleeWeapons.Add(item.Value);
            else if (item.Value.itemType == "Armor") armors.Add(item.Value);
            else rangeWeapons.Add(item.Value);
        }

        foreach (var item in Managers.Data.MiscDict)
            misc.Add(item.Value);
    }

    void CreateSlots(List<ItemInfo> items)
    {
        ClearSlots();

        for(int i = 0; i < items.Count; i++)
        {
            UI_ShopSlot _ui = Managers.UI.MakeSubItem<UI_ShopSlot>(Get<GameObject>((int)GameObjects.Content).transform);
            _ui.SetItem(items[i]);
            slots.Add(_ui);
        }
        
    }

    void ClearSlots()
    {
        foreach (var slot in slots)
            Destroy(slot.gameObject);

        slots.Clear();
    }


}
