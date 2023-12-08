using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ShopSlot : UI_Base
{
    ItemInfo item;

    enum Texts
    {
        Text_Name,
        Text_Description,
        Text_Price,
    }

    enum Buttons
    {
        Btn_Buy,
    }

    enum Images
    {
        Img_Item,
    }

    void Awake()
    {
        Init();
    }


    public override void Init()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<Button>(typeof(Buttons));

        BindEvent(Get<Button>((int)Buttons.Btn_Buy).gameObject, OnClickedBuy);
    }

    public void SetItem(ItemInfo _item)
    {
        item = _item;
        SetImage();
        SetTexts();
    }

    void SetImage()
    {
        int itemType = (item.itemType == "Misc") ? (int)ItemType.Misc : (int)ItemType.Equip;
        Sprite _sprite = Managers.Resource.Load<Sprite>($"Images/Items/{itemType}/{item.id}");
        Get<Image>((int)Images.Img_Item).sprite = _sprite;

    }
    void SetTexts() 
    {
        Get<TextMeshProUGUI>((int)Texts.Text_Name).text = item.name;
        Get<TextMeshProUGUI>((int)Texts.Text_Description).text = item.itemType == "Armor" ? $"방어력 +{item.power}" : $"공격력 +{item.power}";
        if (item.itemType == "Misc")
            Get<TextMeshProUGUI>((int)Texts.Text_Description).text = "";
        Get<TextMeshProUGUI>((int)Texts.Text_Price).text = $"{item.price}";
    }
    void OnClickedBuy(PointerEventData data)
    {
        int gold = Managers.GetPlayer.Inven.Gold;

        if (gold < item.price)
            return;

        Managers.GetPlayer.Inven.Gold -= item.price;
        Managers.GetPlayer.Inven.GainItem(item);
    }
}
