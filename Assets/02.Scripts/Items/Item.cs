using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equip,
    Misc,
}

public class Item : MonoBehaviour
{
    int id;
    string itemName;
    string description;
    ItemType itemType;
    int power;
    int number;

    public int Id { get { return id; } }
    public string ItemName { get { return itemName; } }
    public string Description { get { return description; } }
    public ItemType IType { get { return itemType; } }
    public int Power { get { return power; } }
    public int Number { get { return number; } set { number = value; } }

    public void SetInfo(ItemInfo _info, int _count)
    {
        id = _info.id;
        itemName = _info.name;
        description = _info.description;
        itemType = (ItemType)_info.itemType;
        power = _info.power;
        number = _count;
    }
}
