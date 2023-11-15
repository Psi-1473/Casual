using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    const int ITEM_NONE = -1;
    List<Item> equips = new List<Item>();
    List<Item> misc = new List<Item>();

    List<List<Item>> items = new List<List<Item>>();
    int gold;
    int expStone;


    public List<List<Item>> Items { get { return items; } }
    public int Gold { get { return gold; } set { gold = value; } }
    public int ExpStone { get { return expStone; } set { expStone = value; } }

    private void Awake()
    {
        // Temp
        gold = 10000;
        expStone = 10000;
        items.Add(equips);
        items.Add(misc);
    }

    public void GainItem(ItemInfo _info, int _count = 1)
    {
        List<Item> _items = items[GetItemTypeId(_info.itemType)];
        int idx = _items.FindIndex(x => x.Id == _info.id);

        Debug.Log($"{_info.itemType}");
        if(idx == ITEM_NONE)
        {
            Item item = new Item();
            item.SetInfo(_info, _count);
            _items.Add(item);
            return;
        }

        _items[idx].Number += _count;
    }

    int GetItemTypeId(string type)
    {
        switch(type)
        {
            case "Misc":
                return 1;
            default:
                return 0;
        }
    }
}
