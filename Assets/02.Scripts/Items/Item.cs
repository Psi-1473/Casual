using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ItemType
{
    Equip,
    Misc,
}

public class Item
{
    int id;
    string itemName;
    string description;
    string type;
    ItemType itemType;
    int power;
    int number;
    int grade;

    public int Id { get { return id; } }
    public string ItemName { get { return itemName; } }
    public string Description { get { return description; } }
    public string ITypeString { get { return type; } }
    public ItemType IType { get { return itemType; } }
    public int Power { get { return power; } }
    public int Number { get { return number; } set { number = value; } }
    public int Grade { get { return grade; } }

    public void SetInfo(ItemInfo _info, int _count)
    {
        id = _info.id;
        itemName = _info.name;
        description = _info.description;
        type = _info.itemType;
        itemType = (type == "Misc") ? ItemType.Misc : ItemType.Equip;
        power = _info.power;
        number = _count;
        grade = _info.grade;
    }
    
    public void SetInfoBySavedData(ItemSaveData _itemData)
    {
        id = _itemData.id;
        itemName = _itemData.itemName;
        description = _itemData.description;
        type = _itemData.type;
        itemType = _itemData.itemType;
        power = _itemData.power;
        number = _itemData.number;
        grade = _itemData.grade;
    }
}
