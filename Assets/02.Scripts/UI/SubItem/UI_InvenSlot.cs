using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InvenSlot : UI_Base
{
    Item item;

    public Item OwningItem { get { return item; } }

    enum GameObjects
    {
        Text_Number,
        Img_Item,
    }

    void Awake()
    {
        Init();
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));

    }

    public void SetItem(Item _item)
    {
        item = _item;
        Get<GameObject>((int)GameObjects.Text_Number).GetComponent<TextMeshProUGUI>().text = $"{_item.Number}";
        Get<GameObject>((int)GameObjects.Img_Item).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>($"Images/Items/{(int)_item.IType}/{_item.Id}");
        // 텍스트 설정
        // 이미지 설정
    }
}
