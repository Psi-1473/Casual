using System;
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
    public int Gold { get { return gold; } set { gold = value; 
            if(OnGoldChanged != null)
                OnGoldChanged.Invoke(); } }
    public int ExpStone { get { return expStone; } set { expStone = value;
            if (OnGoldChanged != null)
                OnGoldChanged.Invoke(); } }

    public Action OnGoldChanged;

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
    public void GainItem(Item _item, int _count = 1)
    {
        List<Item> _items = items[GetItemTypeId(_item.ITypeString)];
        int idx = _items.FindIndex(x => x.Id == _item.Id);

        if (idx == ITEM_NONE)
        {
            _items.Add(_item);
            return;
        }

        _items[idx].Number += _count;
    }
    public void GainSavedItem(ItemSaveData _item)
    {
        List<Item> _items = items[GetItemTypeId(_item.type)];
        Item item = new Item();
        item.SetInfoBySavedData(_item);
        _items.Add(item);

    }

    public void RemoveItem(Item _item, int _number = 1)
    {
        List<Item> _items = items[GetItemTypeId(_item.ITypeString)];
        int index = _items.FindIndex(x => x.Id == _item.Id);

        if (index == -1) return;

        if (_items[index].Number > _number)
            _items[index].Number--;
        else _items.RemoveAt(index);
    }

    public Item ItemById(ItemType _type, int _id)
    {
        return items[(int)_type].Find(x => x.Id == _id);
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
